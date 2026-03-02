using JKFrame;
using Unity.Netcode;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectConfig", menuName = "GenerateConfig/EffectConfig")]
public class EffectConfig : ConfigBase
{
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;

    public GameObject effectPrefab;
    public AudioClip effectAudio;
    public float effTimeOffset;

}
