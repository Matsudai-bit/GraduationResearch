using UnityEngine;

public class DomeController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LocalTimeScaleManager.Instance.SetTimeScale(GetComponent<LocalTimeScaleLayer>(), 0.1f);

        GetComponent<MeshRenderer>().material.SetInt("_Cull", 0);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        var timeLayer = other.gameObject.GetComponent<LocalTimeScaleLayer>();

        if (timeLayer && LocalTimeScaleLayerDefinition.Instance.GetLayerName(timeLayer.TimeScaleLayerMask) != "Player")
        {
            var newMask = GetComponent<LocalTimeScaleLayer>().TimeScaleLayerMask & ~timeLayer.TimeScaleLayerMask;
            timeLayer.SetBitMask(newMask);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        var timeLayer = other.gameObject.GetComponent<LocalTimeScaleLayer>();
        if (timeLayer && LocalTimeScaleLayerDefinition.Instance.GetLayerName(timeLayer.TimeScaleLayerMask) != "Player")
        {
;
            var newMask = GetComponent<LocalTimeScaleLayer>().TimeScaleLayerMask | timeLayer.TimeScaleLayerMask;
            timeLayer.SetBitMask(newMask);
        }


    }

}

