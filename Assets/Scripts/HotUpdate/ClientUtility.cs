using JKFrame;

public static class ClientUtility
{
    public static bool UIWindowExist<T>() where T : UI_WindowBase
    {
        return !(UISystem.GetWindow<T>() == null || !UISystem.GetWindow<T>().gameObject.activeInHierarchy);
    }
}
