using JKFrame;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(NPCDialogConfig), menuName = "GenerateConfig/" + nameof(NPCDialogConfig))]
public class NPCDialogConfig : ConfigBase
{
    [OdinSerialize]
    public List<DialogConfigKV> clipList = new List<DialogConfigKV>();

    public DialogConfig GetDialogConfig(string key)
    {
        DialogConfigKV entry = clipList.Find(e => e.key == key);
        if (entry != null) return entry.value;
        Debug.LogError($"NPCDialogConfig中没有找到key为{key}的DialogConfig");
        return null;
    }
}