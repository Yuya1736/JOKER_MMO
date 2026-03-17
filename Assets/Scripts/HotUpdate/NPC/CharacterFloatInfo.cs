using UnityEngine;

public class CharacterFloatInfo : MonoBehaviour
{
    [SerializeField] private TextMesh floatInfo;
    [SerializeField] private SpriteRenderer hpSprite;
    private const float hpBarMaxWidth = 2.56f;

    public void Init(string info)
    {
        floatInfo.text = info;
    }

    private void Update()
    {
        if (Camera.main != null) this.transform.LookAt(Camera.main.transform.position);
    }

    public void ShowHpBar()
    {
        hpSprite.gameObject.SetActive(true);
    }

    public void HideHpBar()
    {
        hpSprite.gameObject.SetActive(false);
    }

    public void UpdateHp(float currentHp, float maxHp)
    {
        float hpPercent = Mathf.Clamp01(currentHp / maxHp);
        hpSprite.size = new Vector2(hpPercent * hpBarMaxWidth, hpSprite.size.y);
    }
}