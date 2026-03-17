using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Unity 专用的全局延时执行工具类
/// </summary>
public static class TimerUtils
{
    // 内部私有的 MonoBehaviour 嵌套类，专门用于运行协程
    private class TimerRunner : MonoBehaviour { }

    private static TimerRunner _runner;

    // 懒加载模式获取运行器
    private static TimerRunner Runner
    {
        get
        {
            if (_runner == null)
            {
                // 创建一个隐藏的游戏对象来挂载协程运行器
                GameObject go = new GameObject("[TimerUtils Runner]");
                _runner = go.AddComponent<TimerRunner>();
                // 确保在切换场景时，这个工具类不会被销毁
                UnityEngine.Object.DontDestroyOnLoad(go);
            }
            return _runner;
        }
    }

    /// <summary>
    /// 在指定延迟后执行方法 (受 Time.timeScale 影响，游戏暂停时计时也会暂停)
    /// </summary>
    /// <param name="delaySeconds">延迟秒数</param>
    /// <param name="action">需要执行的方法</param>
    /// <returns>Coroutine 对象，可用于提前取消</returns>
    public static Coroutine ExecuteAfterDelay(float delaySeconds, Action action)
    {
        if (action == null) return null;
        return Runner.StartCoroutine(DelayCoroutine(delaySeconds, action));
    }

    /// <summary>
    /// 在指定延迟后执行方法 (不受 Time.timeScale 影响，使用真实时间)
    /// </summary>
    public static Coroutine ExecuteAfterDelayUnscaled(float delaySeconds, Action action)
    {
        if (action == null) return null;
        return Runner.StartCoroutine(UnscaledDelayCoroutine(delaySeconds, action));
    }

    /// <summary>
    /// 取消尚未执行的计时器
    /// </summary>
    public static void CancelTimer(Coroutine coroutine)
    {
        if (coroutine != null && _runner != null)
        {
            _runner.StopCoroutine(coroutine);
        }
    }

    // --- 内部协程实现 ---

    private static IEnumerator DelayCoroutine(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    private static IEnumerator UnscaledDelayCoroutine(float delay, Action action)
    {
        yield return new WaitForSecondsRealtime(delay);
        action?.Invoke();
    }
}