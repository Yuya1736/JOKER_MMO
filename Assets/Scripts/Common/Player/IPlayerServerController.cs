using UnityEngine;

public interface IPlayerServerController
{
    public void MoveOnServer(Vector2 dir);
    public void JumpOnServer();
    public void AtkOnServer();
}