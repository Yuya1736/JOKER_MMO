using System;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private Action<Vector3, Quaternion> action;
    private void OnAnimatorMove()
    {
        action?.Invoke(animator.deltaPosition, animator.deltaRotation);
    }

    public void AddAction(Action<Vector3, Quaternion> action)
    {
        this.action += action;
    }

    public void RemoveAction(Action<Vector3, Quaternion> action)
    {
        this.action -= action; 
    }

    public void ClearAction()
    {
        this.action = null; 
    }

    #region AnimationEvent
    private void FootStep()
    {

    }
    #endregion
}