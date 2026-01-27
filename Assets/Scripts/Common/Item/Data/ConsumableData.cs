using Unity.Netcode;

public class ConsumableData : StackableItemDataBase
{
    public override ItemType GetItemType()
    {
        return ItemType.Consumable;
    }

    public override void NetworkSerialize<T>(BufferSerializer<T> serializer)
    {
        base.NetworkSerialize(serializer);
    }
}
