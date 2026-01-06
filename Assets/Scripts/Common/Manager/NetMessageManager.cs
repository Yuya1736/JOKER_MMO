using JKFrame;
using System;
using System.Collections.Generic;
using Unity.Netcode;

public class NetMessageManager : SingletonMono<NetMessageManager>
{
    private CustomMessagingManager CustomMessagingManager => NetManager.Instance.CustomMessagingManager;
    // 按消息头类型来进行回调事件，
    private Dictionary<NetMessageType, Action<ulong, INetworkSerializable>> onReceiveMessageCallbackDic = new Dictionary<NetMessageType, Action<ulong, INetworkSerializable>>();

    // 在NetManager中初始化（在InitClient/InitServer后），否则CustomMessagingManager为null
    public void Init()
    {
        // 绑定接收消息函数
        CustomMessagingManager.OnUnnamedMessage += ReceiveMessage;
    }
    // 接收消息
    private void ReceiveMessage(ulong clientId, FastBufferReader reader)
    {
        reader.ReadValueSafe(out NetMessageType messageType);

        switch (messageType)
        {
            case NetMessageType.C2S_Register:
                C2S_Register C2S_RegisterInfo = new C2S_Register();
                reader.ReadNetworkSerializableInPlace(ref C2S_RegisterInfo);
                TriggerOnReceiveMessageCallback(NetMessageType.C2S_Register, clientId, C2S_RegisterInfo);
                break;
            case NetMessageType.C2S_Login:
                C2S_Login C2S_LoginInfo = new C2S_Login();
                reader.ReadNetworkSerializableInPlace(ref C2S_LoginInfo);
                TriggerOnReceiveMessageCallback(NetMessageType.C2S_Login, clientId, C2S_LoginInfo);
                break;
            case NetMessageType.S2C_Register:
                S2C_Register S2C_RegisterInfo = new S2C_Register();
                reader.ReadNetworkSerializableInPlace(ref S2C_RegisterInfo);
                TriggerOnReceiveMessageCallback(NetMessageType.S2C_Register, clientId, S2C_RegisterInfo);
                break;
            case NetMessageType.S2C_Login:
                S2C_Login S2C_LoginInfo = new S2C_Login();
                reader.ReadNetworkSerializableInPlace(ref S2C_LoginInfo);
                TriggerOnReceiveMessageCallback(NetMessageType.S2C_Login, clientId, S2C_LoginInfo);
                break;
            case NetMessageType.C2S_EnterGame:
                C2S_EnterGame C2S_EnterGameInfo = new C2S_EnterGame();
                reader.ReadNetworkSerializableInPlace(ref C2S_EnterGameInfo);
                TriggerOnReceiveMessageCallback(NetMessageType.C2S_EnterGame, clientId, C2S_EnterGameInfo);
                break;
            case NetMessageType.S2C_Disconnect:
                S2C_Disconnect S2C_DisconnectInfo = new S2C_Disconnect();
                reader.ReadNetworkSerializableInPlace(ref S2C_DisconnectInfo);
                TriggerOnReceiveMessageCallback(NetMessageType.S2C_Disconnect, clientId, S2C_DisconnectInfo);
                break;
            case NetMessageType.C2S_Disconnect:
                C2S_Disconnect C2S_DisconnectInfo = new C2S_Disconnect();
                reader.ReadNetworkSerializableInPlace(ref C2S_DisconnectInfo);
                TriggerOnReceiveMessageCallback(NetMessageType.C2S_Disconnect, clientId, C2S_DisconnectInfo);
                break;
        }
    }

    public void SendMessageToServer<T>(NetMessageType messageType, T data) where T : INetworkSerializable
    {
        using (FastBufferWriter writer = WriteData<T>(messageType, data))
        {
            CustomMessagingManager.SendUnnamedMessage(NetManager.ServerClientId, writer);
        }
    }

    public void SendMessageToClient<T>(ulong clientId, NetMessageType messageType, T data) where T : INetworkSerializable
    {
        using (FastBufferWriter writer = WriteData<T>(messageType, data))
        { 
            CustomMessagingManager.SendUnnamedMessage(clientId, writer);
        }
    }

    public void SendMessageToClients<T>(IReadOnlyList<ulong> clientIds, NetMessageType messageType, T data) where T : INetworkSerializable
    {
        using (FastBufferWriter writer = WriteData<T>(messageType, data))
        {
            CustomMessagingManager.SendUnnamedMessage(clientIds, writer);
        }
    }

    public void SendMessageToAllClients<T>(NetMessageType messageType, T data) where T : INetworkSerializable
    {
        using (FastBufferWriter writer = WriteData<T>(messageType, data))
        {
            CustomMessagingManager.SendUnnamedMessageToAll(writer);
        }
    }

    public void RegisterOnReceiveMessageCallback(NetMessageType messageType, Action<ulong, INetworkSerializable> callback)
    {
        if(onReceiveMessageCallbackDic.ContainsKey(messageType))
        {
            onReceiveMessageCallbackDic[messageType] += callback;
        }
        else
        {
            onReceiveMessageCallbackDic.Add(messageType, callback);
        }
    }

    public void UnRegisterOnReceiveMessageCallback(NetMessageType messageType, Action<ulong, INetworkSerializable> callback)
    {
        if (onReceiveMessageCallbackDic.ContainsKey(messageType))
        {
            onReceiveMessageCallbackDic[messageType] -= callback;
        }
    }

    public void TriggerOnReceiveMessageCallback(NetMessageType messageType, ulong clientId, INetworkSerializable networkSerializable)
    {
        if(onReceiveMessageCallbackDic.TryGetValue(messageType, out Action<ulong, INetworkSerializable> callback))
        {
            callback.Invoke(clientId, networkSerializable);
        }
    }
    // 通过FastBufferWriter来写入消息，CustomMessagingManager.SendUnnamedMessage方法需要传递FastBufferWriter来实现网络通信
    private FastBufferWriter WriteData<T>(NetMessageType messageType, T data) where T : INetworkSerializable
    {
        FastBufferWriter writer = new FastBufferWriter(1024, Unity.Collections.Allocator.Temp);
        writer.WriteValueSafe(messageType);
        writer.WriteValueSafe(data);
        return writer;
    }

}