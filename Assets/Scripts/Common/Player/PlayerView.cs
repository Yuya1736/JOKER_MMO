using JKFrame;
using System;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform weaponRoot;
    private GameObject currentWeapon;

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
    public void SetWeapon(GameObject weaponObj)
    {
        if (currentWeapon != null) currentWeapon.GameObjectPushPool();
        currentWeapon = weaponObj;

        weaponObj.transform.SetParent(weaponRoot, false);
        weaponObj.transform.localPosition = Vector3.zero;
        weaponObj.transform.localEulerAngles = Vector3.zero;
    }

    #region AnimationEvent
    private void FootStep()
    {

    }

    
    #endregion
}