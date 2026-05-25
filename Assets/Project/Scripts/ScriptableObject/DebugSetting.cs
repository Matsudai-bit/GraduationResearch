using UnityEngine;

[CreateAssetMenu(fileName = "DebugSettings", menuName = "DebugSettings")]
public class DebugSettings : ScriptableObject
{
    // ---------------------------------------------------------------
    // シングルトン
    // ---------------------------------------------------------------

    private static DebugSettings s_instance;

    public static DebugSettings Instance
    {
        get
        {
            if (s_instance != null) return s_instance;

            // Resources/DebugSettings.asset から自動ロード
            s_instance = Resources.Load<DebugSettings>("DebugSettings");

            if (s_instance == null)
            {
                Debug.LogError(
                    "[DebugSettings] " +
                    "Resources フォルダに DebugSettings.asset が見つかりません。\n" +
                    "Create > LocalTimeScale > LayerDefinition で作成し Resources/ に配置してください。"
                );
            }

            return s_instance;
        }
    }


    [Header("ヒットストップを適用するかどうか")]
    public bool applyHitStop;
    [Header("ヒットストップの秒数")]
    public float hitStopTime;
    [Header("ヒットストップのスロー倍率")]
    public float hitStopApplyTimeScale;

    [Header("どのくらいで移動するか(s)")]
    public float movingTime;
    [Header("SEを適用するかどうか")]
    public bool applySE;
    [Header("攻撃エフェクトを出すかどうか")]
    public bool applyAttackEffect;
    [Header("ヒットエフェクトを出すかどうか")]
    public bool applyHitEffect;
    [Header("残像を出すかどうか")]
    public bool applyAfterImage;
    [Header("ドームのスロー倍率")]
    public float domeTimeScale;

}
