using UnityEngine;

public interface IHitTarget
{
    public void BeHit(AtkData atkData);
}

public class AtkData
{
    public ulong clientId;

    public int atkValue;
    public Vector3 atkPos;

    public float repelDis;
    public Vector3 repelSourcePos;
}
