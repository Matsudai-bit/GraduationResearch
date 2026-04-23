using UnityEngine;

public class DomeController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        SpeedController.Instance.SetSpeed(GetComponent<SpeedTag>(), 1.0f);
        if (other.gameObject.GetComponent<SpeedTag>())
        {
            var tag = other.gameObject.GetComponent<SpeedTag>();
            tag.SetBitMask( GetComponent<SpeedTag>().SpeedTypeMask - tag.SpeedTypeMask );
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<SpeedTag>())
        {
            var tag = other.gameObject.GetComponent<SpeedTag>();
            tag.SetBitMask(GetComponent<SpeedTag>().SpeedTypeMask + tag.SpeedTypeMask);
        }

        SpeedController.Instance.SetSpeed(GetComponent<SpeedTag>(), 0.1f);

    }

}

