using JKFrame;
using System;
using System.Collections;
using UnityEngine;

public class PlayerClientController : MonoBehaviour, IPlayerClientController
{
    private PlayerController mainPlayerController;
    private Camera mainCamera;
    private Vector2 lastDir = Vector2.zero;
    public Transform cameraLookPos { get; private set; }
    public Transform camaraFollow { get; private set; }
    public PlayerView playerView { get; private set; }
    public bool canControl;
    public void Init()
    {
        mainPlayerController = PlayerManager.playerController;
        mainPlayerController.playerClientController = this;
        mainCamera = Camera.main;
        if (playerView == null) playerView = transform.Find("PlayerView").GetComponent<PlayerView>();
        if (camaraFollow == null) camaraFollow = transform.Find("PlayerView");
        if (cameraLookPos == null) cameraLookPos = camaraFollow.transform.Find("CameraLookPos");
        canControl = true;
        mainPlayerController.onWeaponChanged += playerView.SetWeapon;
        mainPlayerController.onHpChanged += MainController_UpdatePlayerHp;
        playerView.StartSkillHitAcion += OnStartSkillHit;
        //AOIUtility.InitClient(mainPlayerController, AOIUtility.GetChunkCoordByWorldPosition(this.transform.position));
    }

    // 更新客户端面板的血条显示
    private void MainController_UpdatePlayerHp()
    {
        float fillAmount = mainPlayerController.currentHp.Value / mainPlayerController.maxHp;
        UISystem.GetWindow<UI_PlayerInfoWindow>().UpdateHp(fillAmount);
    }

    private void Update()
    {
        ClientMoveInput();
        ClientJumpInput();
        ClientAtkInput();
    }

    private void OnStartSkillHit()
    {
        EffectConfig effectConfig = mainPlayerController.playerAtkConfigs[mainPlayerController.playerAtkIndex.Value].atkEffectConfig;
        PlaySkillEffect(effectConfig);
    }


    private void ClientMoveInput()
    {
        Vector2 dir = Vector2.zero;
        if (canControl)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            dir = new Vector2(x, y);
        }

        Vector3 dir3 = new Vector3(dir.x, 0, dir.y);
        float yEuler = mainCamera.transform.eulerAngles.y;
        Vector3 newDir3 = Quaternion.Euler(new Vector3(0, yEuler, 0)) * dir3;
        Vector2 newDir2 = new Vector2(newDir3.x, newDir3.z);
        if (Vector2.Distance(lastDir, newDir2) <= 0.05f) return;
        lastDir = newDir2;

        mainPlayerController.Send_InputInfo_ServerRpc(new Vector2(newDir3.x, newDir3.z));
    }

    private void ClientJumpInput()
    {
        if (!canControl) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mainPlayerController.Send_Jump_ServerRpc();
        }
    }

    private void ClientAtkInput()
    {
        if (!canControl) return;
        if (Input.GetMouseButtonDown(0))
        {
            mainPlayerController.Send_Atk_ServerRpc();
        }
    }
    public void PlaySkillEffect(EffectConfig effectConfig) // 直接通过特效配置来播放，适用于攻击开始时播放atk特效
    {
        // 播放特效
        string effectName = effectConfig.effectPrefab.name;
        print(effectName);

        GameObject effObj = PoolSystem.GetGameObject(effectName);
        if (effObj == null)
        {
            effObj = Instantiate(effectConfig.effectPrefab);
            effObj.name = effectName;
        }    
        effObj.SetActive(true);
        effObj.transform.SetParent(playerView.playerAtkEffTransform);
        effObj.transform.localPosition = effectConfig.position;
        effObj.transform.localRotation = Quaternion.Euler(effectConfig.rotation);
        effObj.transform.localScale = effectConfig.scale;
        effObj.GetComponent<ParticleSystem>().Simulate(effectConfig.effTimeOffset); // 让粒子系统从指定时间点开始播放
        effObj.GetComponent<ParticleSystem>().Play();
        StartCoroutine(DestroySkillEffect(effObj, .5f));
        // 播放音效
        AudioSystem.PlayOneShot(effectConfig.effectAudio, playerView.playerAtkEffTransform);
    }
    public void PlaySkillEffect(Vector3 point) // 播放hit特效，特效位置根据服务器传来的攻击点来定
    {
        EffectConfig effectConfig = mainPlayerController.playerAtkConfigs[mainPlayerController.playerAtkIndex.Value].hitEffectConfig;
        // 播放特效
        string effectName = effectConfig.effectPrefab.name;

        GameObject effObj = PoolSystem.GetGameObject(effectName);
        if (effObj == null)
        {
            effObj = Instantiate(effectConfig.effectPrefab);
            effObj.name = effectName;
        }    
        effObj.SetActive(true);
        effObj.transform.SetParent(playerView.playerAtkEffTransform);
        effObj.transform.position = point;
        effObj.transform.localRotation = Quaternion.Euler(effectConfig.rotation);
        effObj.transform.localScale = effectConfig.scale;
        effObj.GetComponent<ParticleSystem>().Simulate(effectConfig.effTimeOffset); // 让粒子系统从指定时间点开始播放
        effObj.GetComponent<ParticleSystem>().Play();
        StartCoroutine(DestroySkillEffect(effObj, .5f));
        // 播放音效
        AudioSystem.PlayOneShot(effectConfig.effectAudio, playerView.playerAtkEffTransform);
    }

    private IEnumerator DestroySkillEffect(GameObject eff, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        eff.GetComponent<ParticleSystem>().Stop(true);
        eff.SetActive(false);
        eff.GameObjectPushPool();
    }

    private void OnDestroy()
    {
        playerView.StartSkillHitAcion -= OnStartSkillHit;
        mainPlayerController.onWeaponChanged -= playerView.SetWeapon;
    }
}
