using UnityEngine;

public class StruckDownTaskInfo : TaskInfoBase
{
    public string monsterId;
    public int count;
    public override void ConverFromString(string stringValue)
    {
        string[] strs = stringValue.Split(',');
        if (strs.Length < 2) Debug.LogError($"StruckDownTaskInfo ConvetFromString error, stringValue: {stringValue}");
        monsterId = strs[0];
        count = int.Parse(strs[1]);
    }

    public override int GetCount()
    {
        return count;
    }
}
