using JKFrame;
using System;

public class MonsterClientController : CharacterClientControllerBase<MonsterController>, IMonsterClientController, INetworkSideController
{
    protected CharacterFloatInfo _characterFloatInfo;
    public override void Init(MonsterController mainController)
    {
        base.Init(mainController);
        mainController.config = ResSystem.LoadAsset<MonsterConfig>(gameObject.name);
        mainController.clientController = this;
        _characterFloatInfo = transform.Find("HeadPoint").GetComponentInChildren<CharacterFloatInfo>(true);
        _characterFloatInfo.gameObject.SetActive(true);
        _characterFloatInfo.Init(mainController.config.mosterName);
        mainController.view.monsterAttackAction += OnMonsterAtk;
        mainController.currentState.OnValueChanged += OnMonsterDie;
        mainController.HpChangedAction += MainController_UpdateMonsterHp;
        MainController_UpdateMonsterHp(mainController.currentHp.Value, mainController.currentHp.Value);
    }

    private void OnMonsterAtk()
    {
        PlayEffect(mainController.atkEffectConfig);
    }

    private void OnMonsterDie(MonsterState oldState, MonsterState newState)
    {
        if (newState == MonsterState.die)
        {
            _characterFloatInfo.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        mainController.view.monsterAttackAction -= OnMonsterAtk;
        mainController.currentState.OnValueChanged -= OnMonsterDie;
        mainController.HpChangedAction -= MainController_UpdateMonsterHp;
    }

    private void MainController_UpdateMonsterHp(float oldValue, float newValue)
    {
        if (mainController.currentState.Value != MonsterState.die)
        {
            if (_characterFloatInfo == null) _characterFloatInfo = transform.Find("HeadPoint").GetComponentInChildren<CharacterFloatInfo>(true);
            if (_characterFloatInfo.gameObject.activeInHierarchy == false) _characterFloatInfo.gameObject.SetActive(true);
        }
        _characterFloatInfo.UpdateHp(mainController.currentHp.Value, mainController.MaxHp);
    }

}
