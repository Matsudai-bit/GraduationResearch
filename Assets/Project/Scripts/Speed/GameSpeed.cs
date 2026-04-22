using System;
using UnityEngine;

public class GameSpeed 
{
    private float m_speed = 1f;

    private SpeedTag m_tag ;
    public float CurrentSpeed => m_speed;
    public SpeedTag SpeedTag => m_tag;

    public Action<float> OnValueChange;

    public void Initialize(SpeedTag speedTag)
    {
        if (!speedTag)
        {
            Debug.LogError("SpeedTag‚ªnull‚Å‚·");
        }
        m_tag = speedTag;
    }

    public void SetSpeed(float speed)
    {
        m_speed = speed;
        Debug.Log("‘¬“x‚Ì•ÏXF" + speed);
        OnValueChange?.Invoke(speed);
    }
};
    
