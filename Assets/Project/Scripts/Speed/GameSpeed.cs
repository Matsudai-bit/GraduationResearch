using System;
using UnityEngine;

public class GameSpeed 
{
    private float m_speed = 1f;

    private SpeedTag m_tag ;
    public float CurrentSpeed => m_speed;
    public SpeedTag SpeedTag => m_tag;

    public Action<float> OnValueChange;

    public Action<int> onChangeSpeedTag;


    public void Initialize(SpeedTag speedTag)
    {
        if (!speedTag)
        {
            Debug.LogError("SpeedTagがnullです");
        }
        m_tag = speedTag;

        m_tag.onChange = bitMask =>
        {

        };
    }

    public void SetSpeed(float speed)
    {
        m_speed = speed;
        Debug.Log("速度の変更：" + speed);
        OnValueChange?.Invoke(speed);
    }

    /// <summary>
    /// ビットマスクの設定
    /// </summary>
    /// <param name="maskBit"></param>
    public void SetBitMask(int maskBit)
    {
        if ((maskBit & m_tag.SpeedTypeMask) != m_tag.SpeedTypeMask)
        {
            m_tag.SpeedTypeMask = maskBit;
            onChangeSpeedTag?.Invoke(m_tag.SpeedTypeMask);
        }
    }

};
    
