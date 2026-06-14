using Unity.Netcode;
using UnityEngine;

public struct PlayerInputCommand : INetworkSerializable
{
    public uint Tick;
    public Vector2 MoveDir;
    public float Yaw;
    public byte Buttons;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue<uint>(ref Tick);
        serializer.SerializeValue<float>(ref MoveDir.x);
        serializer.SerializeValue<float>(ref MoveDir.y);
        serializer.SerializeValue<float>(ref Yaw);
        serializer.SerializeValue<byte>(ref Buttons);
    }
}
