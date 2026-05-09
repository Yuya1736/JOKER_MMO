using UnityEngine;

public class DialogTaskInfo : TaskInfoBase
{
    public string npcId;
    public string DialogId;
    public override void ConverFromString(string stringValue)
    {
        string[] strs = stringValue.Split(',');
        if (strs.Length < 2) Debug.LogError($"DialogTaskInfo ConvetFromString error, stringValue: {stringValue}");
        npcId = strs[0];
        DialogId = strs[1];
    }

    public override int GetCount()
    {
        return 1;
    }
}
