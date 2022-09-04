using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;

public class DirectionalIndicatorAssignment : MonoBehaviour
{
  
    public GameObject targetUltrasoundPlane;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AssignIndicator()
    {
        var directionalIndicatorScript = gameObject.GetComponent<DirectionalIndicator>();

        directionalIndicatorScript.DirectionalTarget = targetUltrasoundPlane.transform;
    }
}
