using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private HashSet<IHitTarget> hitTargets = new HashSet<IHitTarget>(5);
    private Collider _collider;
    public event Action<IHitTarget, Vector3> onHitTargetAction;

    public void Init(Action<IHitTarget, Vector3> onHitTargetAction)
    {
        this.onHitTargetAction = onHitTargetAction;
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
    }

    public void StartHit()
    {
        hitTargets.Clear();
        _collider.enabled = true;
    }

    public void CloseHit()
    {
        hitTargets.Clear();
        _collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<IHitTarget>(out IHitTarget hitTarget);
        if (hitTarget != null && !hitTargets.Contains(hitTarget))
        {
            hitTargets.Add(hitTarget);
            Vector3 point = other.ClosestPoint(transform.position);
            onHitTargetAction?.Invoke(hitTarget, point);
        }
    }
}
