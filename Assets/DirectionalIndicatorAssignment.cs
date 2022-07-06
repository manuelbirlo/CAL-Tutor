using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;

public class DirectionalIndicatorAssignment : MonoBehaviour
{
    public int PlaneNumber;

    private GameObject targetUltrasoundPlane;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AssignIndicator()
    {
        targetUltrasoundPlane = GameObject.Find("USPlane" + PlaneNumber);

        var directionalIndicatorScript = gameObject.GetComponent<DirectionalIndicator>();

        directionalIndicatorScript.DirectionalTarget = targetUltrasoundPlane.transform;
    }
}
