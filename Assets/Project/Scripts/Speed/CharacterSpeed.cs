using System;
using UnityEngine;

public class CharacterSpeed 
{
    private readonly GameSpeed m_baseSpeed = new();  // 基準の速さ
    public float CurrentSpeed => m_baseSpeed.CurrentSpeed * m_animationSpeed;

    public Action<float> onChangeBaseSpeed;         // ベース速度が変更された時に呼ばれる
    public Action<float> onChangeAnimationSpeed;    // ベース速度が変更された時に呼ばれる

    float m_animationSpeed = 0.0f;

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Initialize(SpeedTag speedTag)
    {
        SpeedController.Instance.Registry(m_baseSpeed);

        m_baseSpeed.Initialize(speedTag);

        // ベース速度の値が変更された時の処理を登録
        m_baseSpeed.OnValueChange = baseSpeed =>
        {
            // 
            onChangeBaseSpeed       ?.Invoke(baseSpeed);
            onChangeAnimationSpeed  ?.Invoke(baseSpeed * m_animationSpeed);
        };
    }

    public void SetAnimationSpeed(float speed)
    {
        m_animationSpeed = speed;
        onChangeAnimationSpeed?.Invoke(m_baseSpeed.CurrentSpeed * speed);
    }


}
