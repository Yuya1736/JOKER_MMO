using JKFrame;
using UnityEngine;
using UnityEngine.UI;

public struct RewardData
{
    public string iconKey;
    public int count;
}

public class UI_RewardItem : MonoBehaviour
{
    public Image icon;
    public Text countText;

    public void Init(Sprite icon, int num)
    {
        this.icon.sprite = icon;
        countText.text = $"x {num}";
    }

    public void Destroy()
    {
        this.GameObjectPushPool();
    }
}
