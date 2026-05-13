using System.Collections.Generic;
using UnityEngine;

// エンジン側で種別を管理する ScriptableObject
[CreateAssetMenu(fileName = "LocalTimeScaleLayerDefinition", menuName = "LocalTimeScale/LayerDefinition")]
public class LocalTimeScaleLayerDefinition : ScriptableObject
{
    // ---------------------------------------------------------------
    // シングルトン
    // ---------------------------------------------------------------

    private static LocalTimeScaleLayerDefinition s_instance;

    public static LocalTimeScaleLayerDefinition Instance
    {
        get
        {
            if (s_instance != null) return s_instance;

            // Resources/LocalTimeScaleLayerDefinition.asset から自動ロード
            s_instance = Resources.Load<LocalTimeScaleLayerDefinition>("LocalTimeScaleLayerDefinition");

            if (s_instance == null)
            {
                Debug.LogError(
                    "[LocalTimeScaleLayerDefinition] " +
                    "Resources フォルダに LocalTimeScaleLayerDefinition.asset が見つかりません。\n" +
                    "Create > LocalTimeScale > LayerDefinition で作成し Resources/ に配置してください。"
                );
            }

            return s_instance;
        }
    }

    // ---------------------------------------------------------------
    // データ
    // ---------------------------------------------------------------

    [SerializeField] private List<string> m_layerNames = new() { "A", "B", "C" };

    public IReadOnlyList<string> LayerNames => m_layerNames;

    // ---------------------------------------------------------------
    // 名前 ↔ ビットフラグ の変換
    // ---------------------------------------------------------------

    /// <summary>
    /// レイヤー名からビットフラグ（単一ビット）を取得する
    /// </summary>
    /// <param name="layerName">レイヤー名</param>
    /// <returns>対応するビットフラグ。見つからない場合は 0</returns>
    public int GetBitFlag(string layerName)
    {
        int index = m_layerNames.IndexOf(layerName);

        if (index < 0)
        {
            Debug.LogWarning($"[LocalTimeScaleLayerDefinition] レイヤー名 '{layerName}' が見つかりません。");
            return 0;
        }

        return 1 << index;
    }

    /// <summary>
    /// 複数のレイヤー名からビットマスクを取得する
    /// </summary>
    /// <param name="layerNames">レイヤー名の配列</param>
    /// <returns>OR 結合したビットマスク</returns>
    public int GetBitMask(params string[] layerNames)
    {
        int mask = 0;
        foreach (var name in layerNames)
        {
            mask |= GetBitFlag(name);
        }
        return mask;
    }

    /// <summary>
    /// ビットフラグからレイヤー名を取得する（単一ビット）
    /// </summary>
    /// <param name="bitFlag">単一ビットのフラグ</param>
    /// <returns>対応するレイヤー名。見つからない場合は空文字</returns>
    public string GetLayerName(int bitFlag)
    {
        for (int i = 0; i < m_layerNames.Count; i++)
        {
            if (bitFlag == (1 << i)) return m_layerNames[i];
        }

        Debug.LogWarning($"[LocalTimeScaleLayerDefinition] ビットフラグ '{bitFlag}' に対応するレイヤーが見つかりません。");
        return string.Empty;
    }

    /// <summary>
    /// ビットマスクに含まれる全レイヤー名を取得する
    /// </summary>
    /// <param name="bitMask">ビットマスク</param>
    /// <returns>含まれるレイヤー名のリスト</returns>
    public List<string> GetLayerNames(int bitMask)
    {
        var names = new List<string>();
        for (int i = 0; i < m_layerNames.Count; i++)
        {
            if ((bitMask & (1 << i)) != 0)
            {
                names.Add(m_layerNames[i]);
            }
        }
        return names;
    }
}
