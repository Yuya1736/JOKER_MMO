using JKFrame;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(DialogConfig), menuName = "GenerateConfig/" + nameof(DialogConfig))]
public class DialogConfig : ConfigBase
{
    public List<DialogClip> clipList = new List<DialogClip>();
}

public class DialogClip
{
    public string name;
    public string content;
}

public class DialogConfigKV
{
    public string key;
    public DialogConfig value;
}
