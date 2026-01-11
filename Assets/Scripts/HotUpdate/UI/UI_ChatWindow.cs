using JKFrame;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ChatWindow : UI_CustomWindowBase, IPointerEnterHandler, IPointerExitHandler, IInputBlockerUI
{
    [SerializeField] private InputField inputField;
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private Transform contentRoot;
    // 获取背景，在鼠标不进入聊天栏时，聊天栏背景需要为透明状态
    [SerializeField] private Image imgMainBk;
    [SerializeField] private Image imgInputBk;
    [SerializeField] private ScrollRect scrollRect;

    [SerializeField] private int maxChatItemNum = 50;
    private Queue<UI_ChatWindowItem> chatItemQueue = new Queue<UI_ChatWindowItem>(55);

    // 这里重写Enable，打断原本的Blocker逻辑，因为聊天栏在使用时才会是Blocker，并非激活时
    public override void OnEnable() { }
    public override void OnDisable() { }

    private void Awake()
    {
        inputField.onSubmit.AddListener(OnInputSubmit);
        NetMessageManager.Instance.RegisterOnReceiveMessageCallback(NetMessageType.S2C_Chat, OnReceiveChatMessage);

        // 初始UI为透明
        imgInputBk.color = new Color32(0, 0, 0, 0);
        imgMainBk.color = new Color32(0, 0, 0, 0);
        scrollRect.vertical = false;
    }

    private void OnReceiveChatMessage(ulong arg1, INetworkSerializable serializable)
    {
        S2C_Chat s2C_ChatInfo = (S2C_Chat)serializable;
        string name = s2C_ChatInfo.name;
        string info  = s2C_ChatInfo.info;
        switch (s2C_ChatInfo.errorType)
        {
            case NetMessageErrorCode.None:
                AddItem(name, info);
                break;
            default:
                break;
        }
    }

    private void OnInputSubmit(string info)
    {
        inputField.text = "";
        inputField.Select();
        inputField.ActivateInputField();
        // 发送消息到服务端，服务端分发
        NetMessageManager.Instance.SendMessageToServer<C2S_Chat>(NetMessageType.C2S_Chat, new C2S_Chat
        {
            info = info
        });
        // 自身客户端消息显示在右边
        AddItem("我", info, true);
    }

    [Button]
    private void AddItem(string name, string info, bool isOwner = false)
    {
        bool isEnd = scrollbar.value <= 0.1;
        UI_ChatWindowItem item = ResSystem.InstantiateGameObject<UI_ChatWindowItem>(contentRoot, nameof(UI_ChatWindowItem));
        item.Init(name, info, isOwner);
        if(chatItemQueue.Count > maxChatItemNum)
        {
            UI_ChatWindowItem _item = chatItemQueue.Dequeue();
            _item.GameObjectPushPool();
        }
        chatItemQueue.Enqueue(item);
        if (isEnd) StartCoroutine(ScrollbarToEnd());
    }

    private IEnumerator ScrollbarToEnd()
    {
        const int waitFrame = 3;
        for(int i = 1;i <= waitFrame;++ i)
            yield return null;
        scrollbar.value = 0;
    }

    // 鼠标不进入背景就是透明的
    public void OnPointerExit(PointerEventData eventData)
    {
        imgInputBk.color = new Color32(0, 0, 0, 0);
        imgMainBk.color = new Color32(0, 0, 0, 0);
        scrollRect.vertical = false;
        JKFrame.EventSystem.TypeEventTrigger<CheckUIInputBlockerEvent>(new CheckUIInputBlockerEvent(this, false));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        imgInputBk.color = new Color32(0, 0, 0, 159);
        imgMainBk.color = new Color32(0, 0, 0, 67);
        scrollRect.vertical = true;
        JKFrame.EventSystem.TypeEventTrigger<CheckUIInputBlockerEvent>(new CheckUIInputBlockerEvent(this, true));
    }
}
