using System;
using Unity.Netcode;
using UnityEngine.Scripting;

public static class NetworkVariableSerializationBinder
{
    [Preserve]
    private static void EnsureAOT()
    {
        // 强制 AOT 编译器识别 NetworkVariable<PlayerState> 和 NetVariable<PlayerState> 的泛型实例化
        NetworkVariable<PlayerState> _ = new NetworkVariable<PlayerState>(PlayerState.None);
        NetVariable<PlayerState> __ = new NetVariable<PlayerState>(PlayerState.None);
    }

    public static void Init()
    {
        BindNetworkVariableSerialization<PlayerState>();
    }

    public static void BindNetworkVariableSerialization<T>() where T : unmanaged, Enum
    {
        UserNetworkVariableSerialization<T>.WriteValue = (FastBufferWriter writer, in T value) =>
        {
            writer.WriteValueSafe(value);
        };

        UserNetworkVariableSerialization<T>.ReadValue = (FastBufferReader reader, out T value) =>
        {
            reader.ReadValueSafe(out value);
        };

        UserNetworkVariableSerialization<T>.DuplicateValue = (in T value, ref T duplicatedValue) =>
        {
            duplicatedValue = value;
        };
    }
}