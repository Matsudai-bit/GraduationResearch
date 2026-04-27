using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// 固有の時間
/// </summary>
public class LocalClock
{
    private readonly LocalTimeScale m_timeScale;    // 

    float m_time;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="timeScale">タイムスケール</param>
    public LocalClock(LocalTimeScale timeScale)
    {
        m_timeScale = timeScale;
        m_time = 0.0f;
    }

    /// <summary>
    /// 時差の取得
    /// </summary>
    public float DeltaTime => Time.deltaTime * m_timeScale.TimeScale;

    /// <summary>
    /// 更新処理
    /// </summary>
    public void Update()
    {
        m_time += DeltaTime;
    }
}
