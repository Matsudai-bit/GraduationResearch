using UnityEngine;

public class DomeController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpeedController.Instance.SetSpeed(GetComponent<SpeedTag>(), 0.1f);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<SpeedTag>())
        {
            var tag = other.gameObject.GetComponent<SpeedTag>();
            var newMask = GetComponent<SpeedTag>().SpeedTypeMask & ~tag.SpeedTypeMask;
            tag.SetBitMask(newMask);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<SpeedTag>())
        {
            var tag = other.gameObject.GetComponent<SpeedTag>();
            var newMask = GetComponent<SpeedTag>().SpeedTypeMask | tag.SpeedTypeMask;
            tag.SetBitMask(newMask);
        }


    }

}

