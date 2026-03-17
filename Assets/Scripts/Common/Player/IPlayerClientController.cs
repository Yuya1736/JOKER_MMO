using UnityEngine;

public interface IPlayerClientController : ICharacterClientController
{
    public void PlayPlayerAtkEffect(Vector3 point);
}