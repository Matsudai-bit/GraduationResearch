using System;
using UnityEngine;

[Serializable]
public class SpeedTag : MonoBehaviour
{
    [SerializeField] private SpeedTypeDefinition m_definition;

    // MaskField で選択されたビットマスク
    [SerializeField] private int m_speedTypeMask;

    public Action<int> onChange;
    public int SpeedTypeMask
    {
        get
        {

            return m_speedTypeMask;
        }
        set
        {

            m_speedTypeMask = value;
        }
    }

    /// <summary>
    /// ビットマスクの設定
    /// </summary>
    /// <param name="maskBit"></param>
    public void SetBitMask(int maskBit)
    {
        if ((maskBit & m_speedTypeMask) != m_speedTypeMask)
        {
            m_speedTypeMask = maskBit;
            onChange?.Invoke(m_speedTypeMask);
        }
    }

    public SpeedTypeDefinition Definition => m_definition;

    /// <summary>
    /// 指定したビットフラグが立っているかどうか
    /// </summary>
    /// <param name="bitIndex"></param>
    /// <returns></returns>
    public bool HasType(int bitIndex) => (m_speedTypeMask & (1 << bitIndex)) != 0;
}