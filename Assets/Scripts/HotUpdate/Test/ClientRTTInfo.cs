using JKFrame;
using System.Collections.Generic;
using UnityEngine;

public class ClientRTTInfo : SingletonMono<ClientRTTInfo>
{
    public int RttMs { get; private set; }
    [SerializeField] private int calFrame = 100;
    private Queue<int> queue;
    private int totalMs;

    public void Init()
    {
        queue = new Queue<int>(calFrame);
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        
        if(NetManager.Instance && NetManager.Instance.IsConnectedClient)
        {
            int nowMs = (int)NetManager.Instance.unityTransport.GetCurrentRtt(NetManager.ServerClientId);
            queue.Enqueue(nowMs);
            if(queue.Count > calFrame) totalMs -= queue.Dequeue();
            totalMs += nowMs;
            RttMs = totalMs / queue.Count;
        }
    }
}
