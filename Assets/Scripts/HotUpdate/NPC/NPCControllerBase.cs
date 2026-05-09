#if !UNITY_SERVER || UNITY_EDITOR
using DG.Tweening;
using JKFrame;
using System;
using UnityEngine;

public class NPCControllerBase : MonoBehaviour
{
    [SerializeField] protected string configKey;
    [SerializeField] private GameObject headIcon;

    private void Start()
    {
        if (headIcon != null) headIcon.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (PlayerManager.playerController != null && other.gameObject == PlayerManager.playerController.gameObject)
        {
            headIcon.SetActive(true);
            _isInRange = true;
            //UISystem.Show<UI_PressEInteract>();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (PlayerManager.playerController != null && other.gameObject == PlayerManager.playerController.gameObject && headIcon != null) 
            headIcon.transform.LookAt(PlayerManager.Instance.FreeLook.transform);

        if (_isInteracting && _CameraLookpos != Vector3.zero) 
            Camera.main.transform.LookAt(_CameraLookpos);

        if (_isInRange)
        {
            //if (!UISystem.GetWindow<UI_PressEInteract>()) UISystem.Show<UI_PressEInteract>();
            if (!_isInteracting && Input.GetKeyDown(KeyCode.E))
            {
                _isInteracting = true;
                EnterInteract();
            }
        }
    }

    protected virtual void Update()
    {
        
    }

    private Transform freeLookFollow;
    public NPCDialogConfig npcDialogConfig;
    private float _cameraPosY = 1.8f;
    private float _cameraOffset = 3f; // 这是Camera在侧面的距离
    private float _cameraLookPosOffset = 0.8f; // CameraLookpos相对于centerPos的高度偏移
    private Sequence _cameraSequence;
    private bool _isInteracting = false;
    private Vector3 _CameraLookpos;
    private bool _isInRange = false;
    protected DialogConfig currentDialogConfig;
    //private GameObject _tempGameObject;

    public virtual void StartDialog(DialogConfig config, Action onDialogEnd)
    {
        UISystem.Show<UI_DialogWindow>().Show(config, onDialogEnd);
        currentDialogConfig = config;
    }
    public virtual void EnterInteract()
    {
        // if (UISystem.GetWindow<UI_PressEInteract>()) UISystem.Close<UI_PressEInteract>();
        freeLookFollow = PlayerManager.Instance.FreeLook.Follow;

        PlayerManager.playerClientController.canControl = false;
        // 看向对方
        DialogUtility.StartLookEach(PlayerManager.playerController.transform, transform);
        // 计算摄像机目标位置，保持在玩家和NPC的中点，并且高度为cameraPosY，并且在侧面cameraOffset的距离
        Vector3 centerPos = (PlayerManager.playerController.transform.position + gameObject.transform.position) / 2;
        Vector3 offsetDir = Vector3.Cross(gameObject.transform.position - PlayerManager.playerController.transform.position, Vector3.down).normalized;
        Vector3 targetPos = new Vector3(centerPos.x, PlayerManager.playerController.transform.position.y + _cameraPosY, centerPos.z) + offsetDir * _cameraOffset;
        _CameraLookpos = centerPos + new Vector3(0, _cameraPosY - _cameraLookPosOffset, 0);

        //if(_tempGameObject == null) _tempGameObject = new GameObject("CameraLookTarget"); 
        //_tempGameObject.transform.position = _CameraLookpos;

        //PlayerManager.Instance.FreeLook.Follow = _tempGameObject.transform;
        PlayerManager.Instance.FreeLook.enabled = false;
        _cameraSequence?.Kill();
        _cameraSequence = DOTween.Sequence();
        _cameraSequence.Join(Camera.main.transform.DOMove(targetPos, .5f));

        for (int i = 0; i < PlayerManager.taskDatas.taskDataList.Count; i++)
        {
            TaskData taskData = PlayerManager.taskDatas.taskDataList[i];
            TaskConfig taskConfig = ResSystem.LoadAsset<TaskConfig>(taskData.taskConfigId);
            TaskInfoBase taskInfo = taskConfig.taskInfo;
            if (taskInfo is DialogTaskInfo)
            {
                DialogTaskInfo info = (DialogTaskInfo)taskInfo;
                if (info.npcId == gameObject.name)
                {
                    var dialogId = info.DialogId;
                    //print(dialogId);
                    StartDialog(npcDialogConfig.GetDialogConfig(dialogId), ExitInteract);
                    //UISystem.Show<UI_DialogWindow>().Show(npcDialogConfig.GetDialogConfig(dialogId), ExitInteract);
                }
            }
        }
    }

    public virtual void ExitInteract()
    {
        _isInteracting = false;
        _CameraLookpos = Vector3.zero;
        //if (!UISystem.GetWindow<UI_PressEInteract>() && _isInRange) UISystem.Show<UI_PressEInteract>();
        _cameraSequence?.Kill();
        //PlayerManager.Instance.FreeLook.Follow = freeLookFollow;
        PlayerManager.Instance.FreeLook.enabled = true;
        //CameraManager.Instance._cameraFollowPlayer = true;

        PlayerManager.playerClientController.canControl = true;
        DialogUtility.EndLookEach(PlayerManager.playerController.transform, transform);

        CheckDialogTaskCompeleted();
    }

    public void CheckDialogTaskCompeleted()
    {
        for (int i = 0; i < PlayerManager.taskDatas.taskDataList.Count; i++)
        {
            TaskData taskData = PlayerManager.taskDatas.taskDataList[i];
            TaskConfig taskConfig = ResSystem.LoadAsset<TaskConfig>(taskData.taskConfigId);
            TaskInfoBase taskInfo = taskConfig.taskInfo;
            if (taskInfo is DialogTaskInfo)
            {
                DialogTaskInfo info = (DialogTaskInfo)taskInfo;
                if (info.npcId == gameObject.name)
                {
                    var dialogId = info.DialogId;
                    if (npcDialogConfig.GetDialogConfig(dialogId) == currentDialogConfig)
                    {
                        PlayerManager.Instance.DialogTaskCompeleted(i);
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (PlayerManager.playerController != null && other.gameObject == PlayerManager.playerController.gameObject)
        {
            headIcon.SetActive(false);
            _isInRange = false;
            //UISystem.Close<UI_PressEInteract>();
        }
    }
}
#endif