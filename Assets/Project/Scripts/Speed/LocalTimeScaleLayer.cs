using System;
using UnityEngine;

[Serializable]
public class LocalTimeScaleLayer : MonoBehaviour
{
    [SerializeField] private LocalTimeScaleLayerDefinition m_definition;

    // MaskField で選択されたビットマスク
    [SerializeField] private int m_timeScaleLayerMask;

    public Action<int> onChange;
    public int TimeScaleLayerMask
    {
        get
        {

            return m_timeScaleLayerMask;
        }
    }

    /// <summary>
    /// ビットマスクの設定
    /// </summary>
    /// <param name="maskBit"></param>
    public void SetBitMask(int maskBit)
    {
        if (maskBit != m_timeScaleLayerMask)
        {
            m_timeScaleLayerMask = maskBit;
            Debug.Log("マスクの更新 : " + m_timeScaleLayerMask);
            onChange?.Invoke(m_timeScaleLayerMask);
        }
    }

    public LocalTimeScaleLayerDefinition Definition => m_definition;

    /// <summary>
    /// 指定したビットフラグが立っているかどうか
    /// </summary>
    /// <param name="bitIndex"></param>
    /// <returns></returns>
    public bool HasType(int bitIndex) => (m_timeScaleLayerMask & (1 << bitIndex)) != 0;
}