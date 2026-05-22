using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

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

    private void OnEnable()
    {
        m_domeInObjects.Clear();
    }

    private void OnDisable()
    {
        foreach (var element in m_domeInObjects)
        {
            if (element == null) continue;
            var timeLayer = element.GetComponent<LocalTimeScaleLayer>();

            if (timeLayer && !LocalTimeScaleLayerDefinition.Instance.GetLayerNames(timeLayer.TimeScaleLayerMask).Contains("Player"))
            {
                // Domeïîï™ÇçÌèúÇ∑ÇÈ
                var newMask = ~GetComponent<LocalTimeScaleLayer>().TimeScaleLayerMask & timeLayer.TimeScaleLayerMask;
                timeLayer.SetBitMask(newMask);

            }

        }
        m_domeInObjects.Clear();
    }

    private void OnTriggerExit(Collider other)
    {
        var timeLayer = other.gameObject.GetComponent<LocalTimeScaleLayer>();

        if (timeLayer && !LocalTimeScaleLayerDefinition.Instance.GetLayerNames(timeLayer.TimeScaleLayerMask).Contains("Player"))
        {
            if (other.gameObject.GetComponent<EnemyController>() && !other.gameObject.GetComponent<EnemyController>().IsAlive)
            {
                return;
            }
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
        if (timeLayer && !LocalTimeScaleLayerDefinition.Instance.GetLayerNames(timeLayer.TimeScaleLayerMask).Contains("Player"))
        {
            if (other.GetComponent<EnemyController>() && !other.GetComponent<EnemyController>().IsAlive)
            {
                return;
            }

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

