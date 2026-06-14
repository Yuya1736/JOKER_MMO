using UnityEngine;

public interface IPlayerServerController : ICharacterServerController
{
    public void MoveOnServer(Vector2 dir);
    public void AtkOnServer();
    public void JumpOnServer();
    public PlayerStateSnapshot ProcessPredictedMove(ulong clientId, PlayerInputCommand input);
}
