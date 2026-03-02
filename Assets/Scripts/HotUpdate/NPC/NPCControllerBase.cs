#if !UNITY_SERVER || UNITY_EDITOR
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
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (PlayerManager.playerController != null && other.gameObject == PlayerManager.playerController.gameObject && headIcon != null) headIcon.transform.LookAt(PlayerManager.Instance.FreeLook.transform);
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
        }
    }

    public virtual void OnInteract()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if (PlayerManager.playerController != null && other.gameObject == PlayerManager.playerController.gameObject)
        {
            headIcon.SetActive(false);
        }
    }
}
#endif