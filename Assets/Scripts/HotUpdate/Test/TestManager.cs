//using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public GameObject testServerObject;
    public NetworkObject networkObject;

    private void Update()
    {
#if UNITY_SERVER || UNITY_EDITOR
        if (networkObject == null && Input.GetKeyDown(KeyCode.S))
        {
            networkObject = NetManager.Instance.SpawnObject(NetManager.ServerClientId, testServerObject, Vector3.zero, new Quaternion());
        }
        if (networkObject != null && Input.GetKeyDown(KeyCode.D))
        {
            print(networkObject);
            NetManager.Instance.DeSpawnObject(networkObject);
            networkObject = null;
        }
#endif
    }

    private void Start()
    {
//#if UNITY_SERVER || SERVER_EDITOR_TEST
//            NetManager.Instance.SpawnObject(NetManager.ServerClientId, testServerObject, Vector3.zero);
//#endif
    }


    private void OnGUI()
    {
//#if UNITY_SERVER || UNITY_EDITOR
//            if(TestServerObject.instance != null)
//            {
//                GUILayout.Label("位置：" + TestServerObject.instance.transform.position);
//            }
//#endif

//#if !UNITY_SERVER
//        if (NetManager.Instance == null || PlayerManager.localPlayer == null) return;
//        if (NetManager.Instance.IsConnectedClient)
//        {
//            GUILayout.Label("延迟：" + ClientRTTInfo.Instance.RttMs);
//            GUILayout.Label("位置：" + PlayerManager.localPlayer.transform.position);

//            // dic< 客户端id, 客户端 <隶属于客户端的物体id， 物体> >
//            int serverObjCount = 0;
//            if (NetManager.Instance.SpawnManager.OwnershipToObjectsTable.TryGetValue(NetManager.ServerClientId, out Dictionary<ulong, Unity.Netcode.NetworkObject> serverObjs))
//            {
//                serverObjCount = serverObjs.Count;
//            }

//            GUILayout.Label("服务端对象数量：" + serverObjCount);

//            int otherClients = 0;
//            foreach (KeyValuePair<ulong, Dictionary<ulong, Unity.Netcode.NetworkObject>> item in NetManager.Instance.SpawnManager.OwnershipToObjectsTable)
//            {
//                // 非服务器客户端, 非本地客户端, 即为其余客户端数量
//                if (item.Key != NetManager.ServerClientId && item.Key != NetManager.Instance.LocalClientId)
//                {
//                    otherClients += item.Value.Count;
//                }
//            }
//            GUILayout.Label("其他客户端数量：" + otherClients);
//        }
//#endif
    }
}
