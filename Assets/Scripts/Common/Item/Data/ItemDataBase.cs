using MongoDB.Bson.Serialization.Attributes;
using Unity.Netcode;

[BsonKnownTypes(typeof(WeaponData)), BsonKnownTypes(typeof(ConsumableData)), BsonKnownTypes(typeof(MaterialData))]
public abstract class ItemDataBase : INetworkSerializable
{
    public string configKey;
    public string id = string.Empty;

    public abstract ItemType GetItemType();
    public virtual void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref configKey);
        serializer.SerializeValue(ref id);
    }
}