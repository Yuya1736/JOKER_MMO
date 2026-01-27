using Unity.Netcode;

public class MaterialData : StackableItemDataBase
{
    public override ItemType GetItemType()
    {
        return ItemType.Material;
    }

    public override void NetworkSerialize<T>(BufferSerializer<T> serializer)
    {
        base.NetworkSerialize(serializer);
    }
}
