using JKFrame;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskConfig", menuName = "GenerateConfig/TaskConfig")]
public class TaskConfig : ConfigBase
{
    public string taskName;
    public string taskDescription;
    public string nextTaskId;
    public TaskInfoBase taskInfo;
    public TaskRewardBase taskReward;
    public Vector3 targetPos;
}
