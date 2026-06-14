using JKFrame;
using System;
using UnityEngine;

public class PlayerView : CharacterViewBase
{
    [SerializeField] private Transform weaponRoot;
    private GameObject currentWeapon;
    
    private void OnAnimatorMove()
    {
        //rootMotionAction?.Invoke(animator.deltaPosition, animator.deltaRotation);
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
    public event Action<Vector3, Quaternion> rootMotionAction;
    public event Action JumpStartEndAcion;
    public event Action StartSkillHitAcion;
    public event Action StopSkillHitAcion;
    public event Action SkillCanSwitchAcion;
    public event Action SkillEndAcion;
    public event Action DamageCanControlAction;
    public event Action EquipCanControlAction;
    public event Action EquipEndAction;

    private void FootStep()
    {
        AudioClip audioClip = footStepAudioClips[UnityEngine.Random.Range(0, footStepAudioClips.Length)];
        AudioSystem.PlayOneShot(audioClip, transform.position);
    }

    private void OnJumpStartEnd()
    {
        JumpStartEndAcion?.Invoke();
    }

    private void StartSkillHit()
    {
        StartSkillHitAcion?.Invoke();
    }

    private void StopSkillHit()
    {
        StopSkillHitAcion?.Invoke();
    }

    private void SkillCanSwitch()
    {
        SkillCanSwitchAcion?.Invoke();
    }

    private void SkillEnd()
    {
        SkillEndAcion?.Invoke();
    }

    private void DamageCanControl()
    {
        DamageCanControlAction?.Invoke();
    }

    private void EquipCanControl()
    {
        EquipCanControlAction?.Invoke();
    }

    private void EquipEnd()
    {
        EquipEndAction?.Invoke();
    }

    #endregion
}