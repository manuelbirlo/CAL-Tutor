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
        GameObject probe = GameObject.Find("GE_Voluson_2D_aligned");

        headStandardPlane.GetComponent<MeshRenderer>().enabled = true;
        abdomenStandardPlane.GetComponent<MeshRenderer>().enabled = true;
        femurStandardPlane.GetComponent<MeshRenderer>().enabled = true;

        headStandardPlane.transform.Find("HeadPlaneMirrored").GetComponent<MeshRenderer>().enabled = true;
        headStandardPlane.transform.Find("HeadLabelForUsImage").GetComponent<MeshRenderer>().enabled = true;
        abdomenStandardPlane.transform.Find("AbdomenPlaneMirrored").GetComponent<MeshRenderer>().enabled = true;
        abdomenStandardPlane.transform.Find("AbdomenLabelForUsImage").GetComponent<MeshRenderer>().enabled = true;
        femurStandardPlane.transform.Find("FemurPlaneMirrored").GetComponent<MeshRenderer>().enabled = true;
        femurStandardPlane.transform.Find("FemurLabelForUsImage").GetComponent<MeshRenderer>().enabled = true;

        headStandardPlane.AddComponent<ObjectManipulator>();
        headStandardPlane.AddComponent<BoundsControl>();

        abdomenStandardPlane.AddComponent<ObjectManipulator>();
        abdomenStandardPlane.AddComponent<BoundsControl>();

        femurStandardPlane.AddComponent<ObjectManipulator>();
        femurStandardPlane.AddComponent<BoundsControl>();

        cube.GetComponent<ObjectManipulator>().enabled = true;
        cube.GetComponent<BoundsControl>().enabled = true;

        probe.GetComponent<ObjectManipulator>().enabled = true;
        probe.GetComponent<BoundsControl>().enabled = true;
        //cube.AddComponent<ObjectManipulator>();
        //cube.AddComponent<BoundsControl>();

        //probe.AddComponent<ObjectManipulator>();
        //probe.AddComponent<BoundsControl>();

    }

    public void DeactivateManualInteraction()
    {
        
        GameObject headStandardPlane = GameObject.Find("HeadPlane");
        GameObject abdomenStandardPlane = GameObject.Find("AbdomenPlane");
        GameObject femurStandardPlane = GameObject.Find("FemurPlane");
        GameObject cube = GameObject.Find("CubeRelativeToProbe");
        GameObject probe = GameObject.Find("GE_Voluson_2D_aligned");

        headStandardPlane.GetComponent<MeshRenderer>().enabled = false;
        abdomenStandardPlane.GetComponent<MeshRenderer>().enabled = false;
        femurStandardPlane.GetComponent<MeshRenderer>().enabled = false;

        headStandardPlane.transform.Find("HeadPlaneMirrored").GetComponent<MeshRenderer>().enabled = false;
        headStandardPlane.transform.Find("HeadLabelForUsImage").GetComponent<MeshRenderer>().enabled = false;
        abdomenStandardPlane.transform.Find("AbdomenPlaneMirrored").GetComponent<MeshRenderer>().enabled = false;
        abdomenStandardPlane.transform.Find("AbdomenLabelForUsImage").GetComponent<MeshRenderer>().enabled = false;
        femurStandardPlane.transform.Find("FemurPlaneMirrored").GetComponent<MeshRenderer>().enabled = false;
        femurStandardPlane.transform.Find("FemurLabelForUsImage").GetComponent<MeshRenderer>().enabled = false;

        Destroy(headStandardPlane.GetComponent<ObjectManipulator>());
        Destroy(headStandardPlane.GetComponent<BoundsControl>());

        Destroy(abdomenStandardPlane.GetComponent<ObjectManipulator>());
        Destroy(abdomenStandardPlane.GetComponent<BoundsControl>());

        Destroy(femurStandardPlane.GetComponent<ObjectManipulator>());
        Destroy(femurStandardPlane.GetComponent<BoundsControl>());

        cube.GetComponent<ObjectManipulator>().enabled = false;
        cube.GetComponent<BoundsControl>().enabled = false;

        probe.GetComponent<ObjectManipulator>().enabled = false;
        probe.GetComponent<BoundsControl>().enabled = false;
    }
}
