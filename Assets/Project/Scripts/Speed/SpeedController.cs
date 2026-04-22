using System.Collections.Generic;
using System.Linq;
using Unity.Android.Gradle;
using UnityEngine;

/// <summary>
/// スピード制御コンポーネント
/// </summary>
public class SpeedController : SingletonMonoBehaviour<SpeedController>
{

    Dictionary<SpeedTag, float> m_speedHolder = new();  // タグと速さの紐づけ

    List<GameSpeed> m_gameSpeeds = new();               // スピードを変更するモノたち

    [SerializeField]
    float speed = 1f;

    private SpeedTag speedTag ;


    float rootSpeed = 0f;

    public void Registry(GameSpeed speed)
    {
        if (!m_gameSpeeds.Contains(speed))
        {
            m_gameSpeeds.Add(speed);
        }

    }
    public void Remove(GameSpeed speed)
    {
        m_gameSpeeds.Remove(speed);

    }
    public void SetSpeed(SpeedTag tag, float speed)
    {
        if (!m_speedHolder.TryAdd(tag, speed))
        {
            m_speedHolder.Remove(tag);
            m_speedHolder.Add(tag, speed);
        }

        Debug.Log("tag : " + tag.SpeedTypeMask);  
        foreach (var element in m_gameSpeeds)
        {
            // 対象のタグかどうか
            if ((element.SpeedTag.SpeedTypeMask & tag.SpeedTypeMask) != 0  )
            {
                element.SetSpeed(speed);
            }
        }
    }

     protected  override void Init() 
    {
        speedTag = GetComponent<SpeedTag>();

    }

    private void Start()
    {
        rootSpeed = speed;

    }

    // Update is called once per frame
    void Update()
    {
        if (!Mathf.Approximately(rootSpeed, speed))
        {
            rootSpeed = speed;
            SetSpeed(speedTag, rootSpeed);
        }
    }
}
