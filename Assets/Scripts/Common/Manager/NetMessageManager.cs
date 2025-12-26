using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
// 枚举类型作为消息头，用于判断消息体类型
public enum MessageType
{
    Test,
}
// 消息体需要继承INetworkSerializable实现自定义序列化方法
public class TestData : INetworkSerializable
{
    public string name;
    public int lv;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        // 没有泛型代表string（或byte）
        serializer.SerializeValue(ref name);
        serializer.SerializeValue<int>(ref lv);
    }
}

public class NetMessageManager : MonoBehaviour
{
    private CustomMessagingManager CustomMessagingManager => NetManager.Instance.CustomMessagingManager;
    // 按消息头类型来进行回调事件，
    private Dictionary<MessageType, Action<ulong, INetworkSerializable>> onReceiveMessageCallbackDic = new Dictionary<MessageType, Action<ulong, INetworkSerializable>>();

    // 在NetManager中初始化（在InitClient/InitServer后），否则CustomMessagingManager为null
    public void Init()
    {
        // 绑定接收消息函数
        CustomMessagingManager.OnUnnamedMessage += ReceiveMessage;
        NetManager.Instance.OnClientConnectedCallback += Instance_OnClientConnectedCallback;
        //RegisterOnReceiveMessageCallback(MessageType.Test, func);
    }

    //private void func(ulong clientId, INetworkSerializable serializable)
    //{
    //    TestData testData = (TestData)serializable;
    //    Debug.Log($"callback: {clientId} + {testData.name}");
    //}
    // 测试客户端启动向服务端发消息
    private void Instance_OnClientConnectedCallback(ulong obj)
    {
        SendMessageToServer<TestData>(MessageType.Test, new TestData
        {
            name = "客户端连接成功",
            lv = 114514
        });
    }
    // 接收消息
    private void ReceiveMessage(ulong clientId, FastBufferReader reader)
    {
        reader.ReadValueSafe<MessageType>(out MessageType messageType);

        switch (messageType)
        {
            case MessageType.Test:
                reader.ReadValueSafe<TestData>(out TestData testData);
                Debug.Log($"Receive testData:{testData.name} + {testData.lv}");
                TriggerOnReceiveMessageCallback(MessageType.Test, clientId, testData);
                break;
            default:
                break;
        }
    }

    private void SendMessageToServer<T>(MessageType messageType, T data) where T : INetworkSerializable
    {
        using (FastBufferWriter writer = WriteData<T>(messageType, data))
        {
            CustomMessagingManager.SendUnnamedMessage(NetManager.ServerClientId, writer);
        }
    }

    private void SendMessageToClient<T>(ulong clientId, MessageType messageType, T data) where T : INetworkSerializable
    {
        using (FastBufferWriter writer = WriteData<T>(messageType, data))
        {
            CustomMessagingManager.SendUnnamedMessage(clientId, writer);
        }
    }

    private void SendMessageToClients<T>(IReadOnlyList<ulong> clientIds, MessageType messageType, T data) where T : INetworkSerializable
    {
        using (FastBufferWriter writer = WriteData<T>(messageType, data))
        {
            CustomMessagingManager.SendUnnamedMessage(clientIds, writer);
        }
    }

    private void SendMessageToAllClients<T>(MessageType messageType, T data) where T : INetworkSerializable
    {
        using (FastBufferWriter writer = WriteData<T>(messageType, data))
        {
            CustomMessagingManager.SendUnnamedMessageToAll(writer);
        }
    }

    private void RegisterOnReceiveMessageCallback(MessageType messageType, Action<ulong, INetworkSerializable> callback)
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

    private void UnRegisterOnReceiveMessageCallback(MessageType messageType, Action<ulong, INetworkSerializable> callback)
    {
        if (onReceiveMessageCallbackDic.ContainsKey(messageType))
        {
            onReceiveMessageCallbackDic[messageType] -= callback;
        }
    }

    private void TriggerOnReceiveMessageCallback(MessageType messageType, ulong clientId, INetworkSerializable networkSerializable)
    {
        if(onReceiveMessageCallbackDic.TryGetValue(messageType, out Action<ulong, INetworkSerializable> callback))
        {
            callback.Invoke(clientId, networkSerializable);
        }
    }
    // 通过FastBufferWriter来写入消息，CustomMessagingManager.SendUnnamedMessage方法需要传递FastBufferWriter来实现网络通信
    private FastBufferWriter WriteData<T>(MessageType messageType, T data) where T : INetworkSerializable
    {
        FastBufferWriter writer = new FastBufferWriter(1024, Unity.Collections.Allocator.Temp);
        writer.WriteValueSafe<MessageType>(messageType);
        writer.WriteValueSafe<T>(data);
        return writer;
    }
}