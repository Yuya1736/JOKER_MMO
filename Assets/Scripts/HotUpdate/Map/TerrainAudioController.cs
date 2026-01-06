using JKFrame;
using UnityEngine;

public class TerrainAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSources;

    private void Start()
    {
        audioSources = GetComponentsInChildren<AudioSource>();
        UpdateTerrainAudioVolume();
        EventSystem.AddTypeEventListener<TerrainAudioVolumeChangeEvent>(OnAudioVolumeChanged);
    }

    private void OnDestroy()
    {
        EventSystem.RemoveTypeEventListener<TerrainAudioVolumeChangeEvent>(OnAudioVolumeChanged);
    }

    private void OnAudioVolumeChanged(TerrainAudioVolumeChangeEvent @event)
    {
        UpdateTerrainAudioVolume();
    }

    private void UpdateTerrainAudioVolume()
    {
        foreach (AudioSource audio in audioSources)
        {
            audio.volume = ClientGlobal.Instance.gameSetting.musicValue;
        }
    }
}
