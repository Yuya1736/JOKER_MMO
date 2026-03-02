using UnityEngine;

public class NPCFloatInfo : MonoBehaviour
{
    [SerializeField] private TextMesh floatInfo;

    public void Init(string info)
    {
        floatInfo.text = info;
    }

    private void Update()
    {
        if (Camera.main != null) this.transform.LookAt(Camera.main.transform.position);
    }
}