using UnityEngine;

public class Enemy : MonoBehaviour, IHitTarget
{
    public void BeHit(AtkData atkData)
    {
        print("BeHit");
    }
}
