using JKFrame;

public class Client
{
    public ulong clientId;
    public PlayerData playerData;
    public ClientState state;
    public PlayerController playerController;

    public void Destroy()
    {
        playerData = null;
        state = default;
        playerController = null;
        this.ObjectPushPool();
    }
}
