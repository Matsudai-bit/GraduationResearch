using UnityEngine;

public class DomeController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LocalTimeScaleManager.Instance.SetTimeScale(GetComponent<LocalTimeScaleLayer>(), 0.1f);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<LocalTimeScaleLayer>())
        {
            var tag = other.gameObject.GetComponent<LocalTimeScaleLayer>();
            var newMask = GetComponent<LocalTimeScaleLayer>().TimeScaleLayerMask & ~tag.TimeScaleLayerMask;
            tag.SetBitMask(newMask);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<LocalTimeScaleLayer>())
        {
            var tag = other.gameObject.GetComponent<LocalTimeScaleLayer>();
            var newMask = GetComponent<LocalTimeScaleLayer>().TimeScaleLayerMask | tag.TimeScaleLayerMask;
            tag.SetBitMask(newMask);
        }


    }

}

