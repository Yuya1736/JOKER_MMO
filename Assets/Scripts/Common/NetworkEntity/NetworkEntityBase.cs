using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkEntityBase : NetworkBehaviour
{
    public INetworkSideController sideController { get; protected set; }
    public static Dictionary<Type, Type> sideControllerDic; // key: mainController type, value: sideController type

    public virtual void Init()
    {
        if (sideController == null && gameObject.GetComponent<INetworkSideController>() == null)
        {
            sideController = (INetworkSideController)gameObject.AddComponent(sideControllerDic[this.GetType()]);
        }
    }
}