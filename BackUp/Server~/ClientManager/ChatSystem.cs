using Unity.Netcode;
using UnityEngine;

public partial class ClientsManager
{
    public void InitChatSystem()
    {
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.C2S_Chat, OnReceiveChatMessage);
    }
    private void OnReceiveChatMessage(ulong clientId, INetworkSerializable serializable)
    {
        C2S_Chat c2S_ChatInfo = (C2S_Chat)serializable;
        Client client = clientIdDic[clientId];
        string playerName = client.playerData.name;
        // 分发给所有Gaming客户端
        foreach (var item in clientStateDic[ClientState.Gaming])
        {
            if (item.clientId == clientId) continue; // 如果对应客户端的ID和发送客户端的ID相同，则不需要发送，发送消息的客户端自身就会显示
            NetMessageManager.Instance.SendMessageToClient<S2C_Chat>(item.clientId, NetMessageType.S2C_Chat, new S2C_Chat
            {
                errorType = NetMessageErrorCode.None,
                name = playerName,
                info = c2S_ChatInfo.info
            });
        }
    }
}
