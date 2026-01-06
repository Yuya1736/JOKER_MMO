using JKFrame;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameSettingsWindow : UI_WindowBase
{
    [SerializeField] private Button btnBack;
    [SerializeField] private Button btnPrev;
    [SerializeField] private Button btnNext;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderMusicEff;

    [SerializeField] private Image imgLanguage;
    [SerializeField] private Sprite ChineseSprite;
    [SerializeField] private Sprite EnglishSprite;

    private void Start()
    {
        Init();
        btnBack.onClick.AddListener(OnBtnBackClick);
        btnPrev.onClick.AddListener(OnBtnPrevClick);
        btnNext.onClick.AddListener(OnBtnNextClick);
        sliderMusic.onValueChanged.AddListener(OnSliderMusicChanged);
        sliderMusicEff.onValueChanged.AddListener(OnSliderMusicEffChanged);
    }

    public override void Init()
    {
        ChangeLanguage(LocalizationSystem.LanguageType);
        sliderMusic.SetValueWithoutNotify(ClientGlobal.Instance.gameSetting.musicValue);
        sliderMusicEff.SetValueWithoutNotify(ClientGlobal.Instance.gameSetting.musicEffValue);
    }

    private void OnSliderMusicChanged(float value)
    {
        ClientGlobal.Instance.gameSetting.musicValue = value;
        EventSystem.TypeEventTrigger<TerrainAudioVolumeChangeEvent>(default);
    }

    private void OnSliderMusicEffChanged(float value)
    {
        ClientGlobal.Instance.gameSetting.musicEffValue = value;
    }

    private void OnBtnNextClick()
    {
        if (LocalizationSystem.LanguageType == LanguageType.SimplifiedChinese)
        {
            ChangeLanguage(LanguageType.English);
        }
        else if (LocalizationSystem.LanguageType == LanguageType.English) 
        {
            ChangeLanguage(LanguageType.SimplifiedChinese);
        }
    }

    private void OnBtnPrevClick()
    {
        if (LocalizationSystem.LanguageType == LanguageType.SimplifiedChinese)
        {
            ChangeLanguage(LanguageType.English);
        }
        else if (LocalizationSystem.LanguageType == LanguageType.English)
        {
            ChangeLanguage(LanguageType.SimplifiedChinese);
        }
    }

    private void OnBtnBackClick()
    {
        ClientGlobal.Instance.SaveGameSetting();
        UISystem.Close<UI_GameSettingsWindow>();
    }

    private void ChangeLanguage(LanguageType language)
    {
        LocalizationSystem.LanguageType = language;
        if (language == LanguageType.English)
        {
            imgLanguage.sprite = EnglishSprite;
        }
        else if(language == LanguageType.SimplifiedChinese)
        {
            imgLanguage.sprite = ChineseSprite;
        }
    }
}
