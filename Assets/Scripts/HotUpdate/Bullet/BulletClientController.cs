using JKFrame;
using System.Collections;
using UnityEngine;

public class BulletClientController : MonoBehaviour, IBulletClientController, INetworkSideController
{
    public BulletController mainController;
    public void Init(BulletController mainController)
    {
        this.mainController = mainController;
        mainController.clientController = this;
    }

    public void PlayHitEffect(EffectConfig effectConfig)
    {
        //// 播放特效
        string effectName = effectConfig.effectPrefab.name;
        GameObject effObj = PoolSystem.GetGameObject(effectName);
        if (effObj == null)
        {
            effObj = Instantiate(effectConfig.effectPrefab);
            effObj.name = effectName;
        }
        effObj.SetActive(true);
        effObj.transform.SetParent(null, true);
        effObj.transform.position = effectConfig.position;
        effObj.transform.localRotation = Quaternion.Euler(effectConfig.rotation);
        effObj.transform.localScale = effectConfig.scale;
        effObj.GetComponent<ParticleSystem>().Simulate(effectConfig.effTimeOffset); // 让粒子系统从指定时间点开始播放
        effObj.GetComponent<ParticleSystem>().Play();
        StartCoroutine(DestroySkillEffect(effObj, .5f));
        // 播放音效
        AudioSystem.PlayOneShot(effectConfig.effectAudio, mainController.bulletBoomEffPos);
    }

    public void PlayHitEffect(Vector3 point) // 播放hit特效，特效位置根据服务器传来的攻击点来定
    {
        EffectConfig effectConfig = mainController.config.boomEffect;
        // 播放特效
        string effectName = effectConfig.effectPrefab.name;

        GameObject effObj = PoolSystem.GetGameObject(effectName);
        if (effObj == null)
        {
            effObj = Instantiate(effectConfig.effectPrefab);
            effObj.name = effectName;
        }
        effObj.SetActive(true);
        effObj.transform.SetParent(null, true);
        effObj.transform.position = point;
        effObj.transform.localRotation = Quaternion.Euler(effectConfig.rotation);
        effObj.transform.localScale = effectConfig.scale;
        effObj.GetComponent<ParticleSystem>().Simulate(effectConfig.effTimeOffset); // 让粒子系统从指定时间点开始播放
        effObj.GetComponent<ParticleSystem>().Play();
        StartCoroutine(DestroySkillEffect(effObj, .5f));
        // 播放音效
        AudioSystem.PlayOneShot(effectConfig.effectAudio, point);
    }

    public IEnumerator DestroySkillEffect(GameObject eff, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        eff.GetComponent<ParticleSystem>().Stop(true);
        eff.SetActive(false);
        eff.GameObjectPushPool();
    }
}