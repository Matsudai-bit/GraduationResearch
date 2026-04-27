using System;
using UnityEngine;

public class LocalTimeScale 
{
    private float m_timeScale = 1f;

    private LocalTimeScaleLayer m_timeScaleLayer ;


    public Action<float> OnValueChange;
    public Action<int> onChangeTimeScaleLayer;


    public float TimeScale => m_timeScale;
    public LocalTimeScaleLayer TimeScaleLayer => m_timeScaleLayer;

    public void Initialize(LocalTimeScaleLayer timeScaleLayer)
    {
        if (!timeScaleLayer)
        {
            Debug.LogError("TimeScaleLayerÇ™nullÇ≈Ç∑");
        }
        m_timeScaleLayer = timeScaleLayer;

        m_timeScaleLayer.onChange = bitMask =>
        {
            onChangeTimeScaleLayer?.Invoke(bitMask);
        };
    }

    public void SetTimeScale(float timeScale)
    {
        m_timeScale = timeScale;
        Debug.Log("ë¨ìxÇÃïœçXÅF" + timeScale);
        OnValueChange?.Invoke(timeScale);
    }


};
    
