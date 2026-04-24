using System.Collections.Generic;
using System.Linq;
using Unity.Android.Gradle;
using UnityEngine;

/// <summary>
/// ローカルタイム制御コンポーネント
/// </summary>
public class LocalTimeScaleManager : SingletonMonoBehaviour<LocalTimeScaleManager>
{

    Dictionary<LocalTimeScaleLayer, float> m_timeScaleHolder = new();  // タグと速さの紐づけ

    List<LocalTimeScale> m_gameTimeScales = new();               // 時間を変更するモノたち

    [SerializeField]
    float timeScale = 1f;

    private LocalTimeScaleLayer timeScaleTag ;


    float rootTimeScale = 0f;

    public void Registry(LocalTimeScale timeScale)
    {
        if (!m_gameTimeScales.Contains(timeScale))
        {
            timeScale.onChangeTimeScaleLayer = bitMask =>
            {
                float totalTimeScale = 1.0f;

                // KeyとValueを同時に取り出す
                foreach (var entry in m_timeScaleHolder)
                {
                    var layerData = entry.Key;
                    var bonusValue = entry.Value;

                    // ビット論理積 (&) で、bitMaskの中にそのタグが含まれているかチェック
                    if ((layerData.TimeScaleLayerMask & bitMask) != 0)
                    {
                        totalTimeScale *= bonusValue;
                    }
                }

                Debug.Log($"速度の更新 : {totalTimeScale} (Mask: {bitMask})");

                // 元の変数 timeScale（オブジェクト）に対して値をセット
                timeScale.SetTimeScale(totalTimeScale);
            };

            m_gameTimeScales.Add(timeScale);
        }

    }
    public void Remove(LocalTimeScale timeScale)
    {
        m_gameTimeScales.Remove(timeScale);

    }
    /// <summary>
    /// 指定レイヤーにタイムスケールを調整する
    /// </summary>
    /// <param name="layer">設定するレイヤー</param>
    /// <param name="timeScale">タイムスケール</param>
    public void SetTimeScale(LocalTimeScaleLayer layer, float timeScale)
    {
        if (!m_timeScaleHolder.TryAdd(layer, timeScale))
        {
            m_timeScaleHolder.Remove(layer);
            m_timeScaleHolder.Add(layer, timeScale);
        }

        Debug.Log("layer : " + layer.TimeScaleLayerMask);  
        foreach (var element in m_gameTimeScales)
        {
            // 対象のタグかどうか
            if ((element.TimeScaleLayer.TimeScaleLayerMask & layer.TimeScaleLayerMask) != 0  )
            {
                element.SetTimeScale(timeScale);
            }
        }
    }

     protected  override void Init() 
    {
        timeScaleTag = GetComponent<LocalTimeScaleLayer>();

    }

    private void Start()
    {
        rootTimeScale = timeScale;

    }

    // Update is called once per frame
    void Update()
    {
        if (!Mathf.Approximately(rootTimeScale, timeScale))
        {
            rootTimeScale = timeScale;
            SetTimeScale(timeScaleTag, rootTimeScale);
        }
    }
}
