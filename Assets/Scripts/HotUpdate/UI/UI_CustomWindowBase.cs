using JKFrame;

public class UI_CustomWindowBase : UI_WindowBase
{
    public virtual void OnEnable()
    {
        EventSystem.TypeEventTrigger<CheckUIInputBlockerEvent>(new CheckUIInputBlockerEvent(this, true));
    }

    public virtual void OnDisable()
    {
        EventSystem.TypeEventTrigger<CheckUIInputBlockerEvent>(new CheckUIInputBlockerEvent(this, false));
    }
}
