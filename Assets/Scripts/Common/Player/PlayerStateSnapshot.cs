using Unity.Netcode;
using UnityEngine;

public struct PlayerStateSnapshot : INetworkSerializable
{
    public ulong ClientId;
    public uint Tick; // 该状态由服务器计算完成的 tick
    public Vector3 Position;
    public Vector3 Velocity;
    public float Yaw;
    public PlayerState State;
    public uint LastProcessedInputTick;
    public bool IsGrounded;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<ulong>(ref ClientId);
        serializer.SerializeValue<uint>(ref Tick);
        serializer.SerializeValue<float>(ref Position.x);
        serializer.SerializeValue<float>(ref Position.y);
        serializer.SerializeValue<float>(ref Position.z);
        serializer.SerializeValue<float>(ref Velocity.x);
        serializer.SerializeValue<float>(ref Velocity.y);
        serializer.SerializeValue<float>(ref Velocity.z);
        serializer.SerializeValue<float>(ref Yaw);
        serializer.SerializeValue<PlayerState>(ref State);
        serializer.SerializeValue<uint>(ref LastProcessedInputTick);
        serializer.SerializeValue<bool>(ref IsGrounded);
    }
}
