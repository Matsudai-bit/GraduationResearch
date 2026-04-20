using System;
using UnityEngine;

public class CharacterSpeed 
{
    private readonly GameSpeed m_baseSpeed = new();  // 基準の速さ
    public float CurrentSpeed => m_baseSpeed.CurrentSpeed ;

    public Action<float> m_onChangeBaseSpeed;   // ベース速度が変更された時に呼ばれる

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Initialize()
    {
        // ベース速度の値が変更された時の処理を登録
        m_baseSpeed.OnValueChange = baseSpeed =>
        {
            // 
            m_onChangeBaseSpeed(baseSpeed);
        };
    }


}
