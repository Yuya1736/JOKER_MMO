using System;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine.Scripting;

public static class NetworkVariableSerializationBinder
{
    [Preserve]
    private static void EnsureAOT()
    {
        // 强制 AOT 编译器识别 NetworkVariable<PlayerState> 和 NetVariable<PlayerState> 的泛型实例化
        NetworkVariable<PlayerState> _ = new NetworkVariable<PlayerState>(PlayerState.None);
        NetVariable<PlayerState> __ = new NetVariable<PlayerState>(PlayerState.None); ;

        NetworkVariable<FixedString32Bytes> _1 = new NetworkVariable<FixedString32Bytes>();
        NetVariable<FixedString32Bytes> __1 = new NetVariable<FixedString32Bytes>();
    }

    public static void Init()
    {
        BindNetworkVariableSerialization<PlayerState>();
        BindFixedfloatSerialization();
        //BindFixedString32BytesSerialization();
    }
    public static void BindFixedfloatSerialization()
    {
        UserNetworkVariableSerialization<float>.WriteValue = (FastBufferWriter writer, in float value) =>
        {
            writer.WriteValueSafe(value);
        };

        UserNetworkVariableSerialization<float>.ReadValue = (FastBufferReader reader, out float value) =>
        {
            reader.ReadValueSafe(out value);
        };

        UserNetworkVariableSerialization<float>.DuplicateValue = (in float value, ref float duplicateValue) =>
        {
            duplicateValue = value;
        };
    }
    public static void BindFixedString32BytesSerialization()
    {
        UserNetworkVariableSerialization<FixedString32Bytes>.WriteValue = (FastBufferWriter writer, in FixedString32Bytes value) =>
        {
            writer.WriteValueSafe(value);
        };

        UserNetworkVariableSerialization<FixedString32Bytes>.ReadValue = (FastBufferReader reader, out FixedString32Bytes value) =>
        {
            reader.ReadValueSafe(out value);
        };

        UserNetworkVariableSerialization<FixedString32Bytes>.DuplicateValue = (in FixedString32Bytes value, ref FixedString32Bytes duplicateValue) =>
        {
            duplicateValue = value;
        };
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