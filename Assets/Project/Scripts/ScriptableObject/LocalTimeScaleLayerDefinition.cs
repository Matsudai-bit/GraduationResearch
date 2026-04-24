using System.Collections.Generic;
using UnityEngine;

// ƒGƒ“ƒWƒ“‘¤‚Åí•Ê‚ğŠÇ—‚·‚é ScriptableObject
[CreateAssetMenu(fileName = "SpeedTypeDefinition", menuName = "Speed/SpeedTypeDefinition")]
public class LocalTimeScaleLayerDefinition : ScriptableObject
{
    [SerializeField] private List<string> m_typeNames = new() { "A", "B", "C" };
    public IReadOnlyList<string> TypeNames => m_typeNames;
}