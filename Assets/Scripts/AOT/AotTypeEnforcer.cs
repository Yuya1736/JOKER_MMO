using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Scripting;

[Preserve]
public class AotTypeEnforcer
{
    [Preserve]
    private void Init()
    {
        new List<Vector2Int>();
    }

    public void EnsureAOT()
    {
        // ��� value �����ͻ����� ClientMapManager �������õ�����
        // ��� value �����ͻ����� ClientMapManager �������õ�����
        // ���������� GameObject��������������ͣ�������ȫһ�£�
        EnsureDictionary<Vector2Int, GameObject>();

        // ����������Զ����� TerrainData
        //EnsureDictionary<Vector2Int, TerrainData>();
    }

    // ������ͷ�����ǿ������ Dictionary<K,V> �����еײ����
    public void EnsureDictionary<K, V>()
    {
        var dic = new Dictionary<K, V>();
        K k = default;
        dic.TryGetValue(k, out V v); // <--- ���д������Ϊ�˽����ı���
        dic[k] = v;
        dic.Remove(k);
        dic.ContainsKey(k);
    }
}
