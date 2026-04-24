using System;
using UnityEngine;

public class LocalTimeScaleHandle 
{
    private readonly LocalTimeScale m_baseTimeScale = new();  // 基準の速さ
    public float CurrentTimeScale => m_baseTimeScale.TimeScale * m_animationTimeScale;

    public Action<float> onChangeBaseTimeScale;         // ベースタイムが変更された時に呼ばれる
    public Action<float> onChangeAnimationTimeScale;    // ベースタイムが変更された時に呼ばれる

    float m_animationTimeScale = 0.0f;

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Initialize(LocalTimeScaleLayer timeScaleLayer)
    {
        LocalTimeScaleManager.Instance.Registry(m_baseTimeScale);

        m_baseTimeScale.Initialize(timeScaleLayer);

        // ベースタイムの値が変更された時の処理を登録
        m_baseTimeScale.OnValueChange = baseTimeScale =>
        {
            // 
            onChangeBaseTimeScale       ?.Invoke(baseTimeScale);
            onChangeAnimationTimeScale  ?.Invoke(baseTimeScale * m_animationTimeScale);
        };
    }

    public void SetAnimationTimeScale(float timeScale)
    {
        m_animationTimeScale = timeScale;
        onChangeAnimationTimeScale?.Invoke(m_baseTimeScale.TimeScale * timeScale);
    }


}
