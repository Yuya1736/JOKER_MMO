using UnityEngine;

public class CollectItemTaskInfo : TaskInfoBase
{
    public string itemId;
    public int count;
    public override void ConverFromString(string stringValue)
    {
        string[] strs = stringValue.Split(',');
        if (strs.Length < 2) Debug.LogError($"DialogTaskInfo ConvetFromString error, stringValue: {stringValue}");
        itemId = strs[0];
        count = int.Parse(strs[1]);
    }

    public override int GetCount()
    {
        return count;
    }
}
