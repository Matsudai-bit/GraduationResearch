using System;
using UnityEngine;

[Serializable]
public class SpeedTag : MonoBehaviour
{
    [SerializeField] private SpeedTypeDefinition m_definition;

    // MaskField で選択されたビットマスク
    [SerializeField] private int m_speedTypeMask;
    public int SpeedTypeMask => m_speedTypeMask;
    public SpeedTypeDefinition Definition => m_definition;

    public bool HasType(int bitIndex) => (m_speedTypeMask & (1 << bitIndex)) != 0;
}