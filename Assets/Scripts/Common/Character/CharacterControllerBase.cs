using System;
using UnityEngine;
using JKFrame;


#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

public class CharacterControllerBase : NetworkEntityBase
{
    [HideInInspector] public CharacterViewBase viewBase;
    public NetVariable<float> currentHp = new NetVariable<float>(100);
    public NetVariable<float> maxHp = new NetVariable<float>(100);
    public float MaxHp => maxHp.Value;

#if UNITY_EDITOR
    [ContextMenu("自动设置Animator")]
    public void SetAnimatorSettings()
    {
        AnimatorController animatorController = (AnimatorController)GetComponentInChildren<Animator>().runtimeAnimatorController;
        animatorController.parameters = null;
        AnimatorStateMachine stateMachine = animatorController.layers[0].stateMachine;
        stateMachine.anyStateTransitions = null;
        foreach (ChildAnimatorState state in stateMachine.states)
        {
            string triggerName = state.state.name;
            AnimatorControllerParameter animatorControllerParameter = new AnimatorControllerParameter()
            {
                name = triggerName,
                type = AnimatorControllerParameterType.Trigger
            };
            animatorController.AddParameter(animatorControllerParameter);
            AnimatorStateTransition animatorStateTransition = stateMachine.AddAnyStateTransition(state.state);
            animatorStateTransition.AddCondition(AnimatorConditionMode.If, 0, triggerName);
        }
    }
#endif
}
public abstract class CharacterControllerBase<V, C, S> : CharacterControllerBase where V : CharacterViewBase where C : ICharacterClientController where S : ICharacterServerController
{
    public V view;
    [HideInInspector] public S serverController;
    [HideInInspector] public C clientController;
    public Action<float, float> HpChangedAction;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        //currentHp.Value = MaxHp;
        //print(currentHp.Value);
        currentHp.OnValueChanged = OnHpChanged;
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
    }

    public abstract void InitHp();

    protected void OnHpChanged(float previousValue, float newValue)
    {
        HpChangedAction?.Invoke(previousValue, newValue);
    }

    public void ChangeHp(float value)
    {
        if(value > 0)
        {
            if (currentHp.Value + value < MaxHp) currentHp.Value += value;
            else currentHp.Value = MaxHp;
        }
        else
        {
            if (currentHp.Value + value > 0) currentHp.Value += value;
            else currentHp.Value = 0;
        }
    }
}
