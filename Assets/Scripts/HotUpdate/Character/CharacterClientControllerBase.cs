using JKFrame;
using System.Collections;
using UnityEngine;

public abstract class CharacterClientControllerBase<M> : MonoBehaviour, ICharacterClientController where M : CharacterControllerBase
{
    public M mainController;

    public virtual void Init(M mainController)
    {
        this.mainController = mainController;       
    }    

    public void PlayEffect(EffectConfig effectConfig) // 直接通过特效配置来播放，适用于攻击开始时播放atk特效
    { 
        // 播放特效
        string effectName = effectConfig.effectPrefab.name;

        GameObject effObj = PoolSystem.GetGameObject(effectName);
        if (effObj == null)
        {
            effObj = Instantiate(effectConfig.effectPrefab);
            effObj.name = effectName;
        }
        effObj.SetActive(true);
        effObj.transform.SetParent(mainController.viewBase.atkEffTransform);
        effObj.transform.localPosition = effectConfig.position;
        effObj.transform.localRotation = Quaternion.Euler(effectConfig.rotation);
        effObj.transform.localScale = effectConfig.scale;
        effObj.GetComponent<ParticleSystem>().Simulate(effectConfig.effTimeOffset); // 让粒子系统从指定时间点开始播放
        effObj.GetComponent<ParticleSystem>().Play();
        StartCoroutine(DestroySkillEffect(effObj, 1f));
        // 播放音效
        AudioSystem.PlayOneShot(effectConfig.effectAudio, mainController.viewBase.atkEffTransform);
    }
    
    public IEnumerator DestroySkillEffect(GameObject eff, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        eff.GetComponent<ParticleSystem>().Stop(true);
        eff.SetActive(false);
        eff.GameObjectPushPool();
    }
}