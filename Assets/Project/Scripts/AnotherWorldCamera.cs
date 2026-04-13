using UnityEngine;

public class AnotherWorldCamera : MonoBehaviour
{

    [SerializeField]
    private float m_value = 30.0f;

    float m_time = 0.0f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_time += Time.deltaTime * 30.0f;

        transform.rotation= Quaternion.Euler(35.0f * Mathf.Sin(Mathf.Deg2Rad * m_time), 0.0f, 0.0f);
    }
}
