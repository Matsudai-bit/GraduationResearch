using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DomeController : MonoBehaviour
{
    List<GameObject> m_domeInObjects = new();

    [SerializeField]
    private float slowSpeed = 1.0f;

    public List<GameObject> DomeInObjects => m_domeInObjects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LocalTimeScaleManager.Instance.SetTimeScale(GetComponent<LocalTimeScaleLayer>(), slowSpeed);

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

        if (m_domeInObjects.Contains(other.gameObject))
        {
            m_domeInObjects.Remove(other.gameObject);
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
        if (!m_domeInObjects.Contains(other.gameObject))
        {
            m_domeInObjects.Add(other.gameObject);
        }

    }

}

