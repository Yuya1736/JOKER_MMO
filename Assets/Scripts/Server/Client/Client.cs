using JKFrame;

public class Client
{
    public ulong clientId;
    public PlayerData playerData;
    public ClientState state;
    public PlayerController playerController;
    public PlayerServerController playerServerController;

    public void Destroy()
    {
        playerData = null;
        state = default;
        playerController = null;
        PoolSystem.PushObject(this);
    }
}
