using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;

public class StandardPlanePlacement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateManualInteraction()
    {
        GameObject headStandardPlane = GameObject.Find("HeadPlane");
        GameObject abdomenStandardPlane = GameObject.Find("AbdomenPlane");
        GameObject femurStandardPlane = GameObject.Find("FemurPlane");
        GameObject cube = GameObject.Find("CubeRelativeToProbe");

        headStandardPlane.AddComponent<ObjectManipulator>();
        headStandardPlane.AddComponent<BoundsControl>();

        abdomenStandardPlane.AddComponent<ObjectManipulator>();
        abdomenStandardPlane.AddComponent<BoundsControl>();

        femurStandardPlane.AddComponent<ObjectManipulator>();
        femurStandardPlane.AddComponent<BoundsControl>();

        //cube.AddComponent<ObjectManipulator>();
        //cube.AddComponent<BoundsControl>();
    }

    public void DeactivateManualInteraction()
    {
        
        GameObject headStandardPlane = GameObject.Find("HeadPlane");
        GameObject abdomenStandardPlane = GameObject.Find("AbdomenPlane");
        GameObject femurStandardPlane = GameObject.Find("FemurPlane");
        //GameObject cube = GameObject.Find("CubeRelativeToProbe");

        Destroy(headStandardPlane.GetComponent<ObjectManipulator>());
        Destroy(headStandardPlane.GetComponent<BoundsControl>());

        Destroy(abdomenStandardPlane.GetComponent<ObjectManipulator>());
        Destroy(abdomenStandardPlane.GetComponent<BoundsControl>());

        Destroy(femurStandardPlane.GetComponent<ObjectManipulator>());
        Destroy(femurStandardPlane.GetComponent<BoundsControl>());

        //Destroy(cube.GetComponent<ObjectManipulator>());
        //Destroy(cube.GetComponent<BoundsControl>());
    }
}
