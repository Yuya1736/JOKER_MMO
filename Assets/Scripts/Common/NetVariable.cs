using Unity.Netcode;

public class NetVariable<T> : NetworkVariable<T>
{
    public NetVariable(T value = default,
            NetworkVariableReadPermission readPerm = DefaultReadPerm,
            NetworkVariableWritePermission writePerm = DefaultWritePerm) : base(value, readPerm, writePerm) { }

    public override bool IsDirty()
    {
        if (NetworkVariableSerialization<T>.AreEqual == null) return false;

        return base.IsDirty();
    }
}