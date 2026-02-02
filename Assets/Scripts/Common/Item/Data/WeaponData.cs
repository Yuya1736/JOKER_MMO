using Unity.Netcode;

public class WeaponData : ItemDataBase
{
    public bool isUsed;

    public override ItemType GetItemType()
    {
        return ItemType.Weapon;
    }

    public override void NetworkSerialize<T>(BufferSerializer<T> serializer)
    {
        base.NetworkSerialize(serializer);
        serializer.SerializeValue<bool>(ref isUsed);
    }
}
