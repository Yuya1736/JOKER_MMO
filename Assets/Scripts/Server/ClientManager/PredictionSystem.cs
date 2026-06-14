using Unity.Netcode;

public partial class ClientsManager
{
    public void InitPredictionSystem()
    {
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.C2S_InputBatch, OnReceiveInputBatch);
    }

    private void OnReceiveInputBatch(ulong clientId, INetworkSerializable serializable)
    {
        if (!clientIdDic.TryGetValue(clientId, out Client client))
        {
            return;
        }

        if (client.playerServerController == null)
        {
            return;
        }

        C2S_InputBatch message = (C2S_InputBatch)serializable;
        if (message.commands == null)
        {
            return;
        }

        for (int i = 0; i < message.count; i++)
        {
            PlayerStateSnapshot snapshot = client.playerServerController.ProcessPredictedMove(clientId, message.commands[i]);
            NetMessageManager.Instance.SendMessageToClient(
                clientId,
                NetMessageType.S2C_OwnerSnapshot,
                new S2C_OwnerSnapshot { snapshot = snapshot });
        }
    }
}
