using System;

public class GameSpeed 
{
    private float m_speed = 1f;
    public float CurrentSpeed => m_speed;

    public Action<float> OnValueChange;

    public void SetSpeed(float speed)
    {
        m_speed = speed;
        OnValueChange?.Invoke(speed);
    }
};
    
