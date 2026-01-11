using JKFrame;

public struct TerrainAudioVolumeChangeEvent
{
 
}
public struct CheckUIInputBlockerEvent
{
    public UI_WindowBase uI_Window;
    public bool isEnter;   

    public CheckUIInputBlockerEvent(UI_WindowBase window, bool isEnter)
    {
        this.uI_Window = window;
        this.isEnter = isEnter;
    }
}