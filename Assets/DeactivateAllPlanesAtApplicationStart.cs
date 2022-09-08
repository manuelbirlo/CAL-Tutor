using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAllPlanesAtApplicationStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var headStandardPlane = GameObject.Find("HeadPlane");
        var abdomenStandardPlane = GameObject.Find("AbdomenPlane");
        var femurStandardPlane = GameObject.Find("FemurPlane");

        // csvReaderScript.DeactivateLineRendering();

        headStandardPlane.GetComponent<MeshRenderer>().enabled = false;
        abdomenStandardPlane.GetComponent<MeshRenderer>().enabled = false;
        femurStandardPlane.GetComponent<MeshRenderer>().enabled = false;

        headStandardPlane.transform.Find("HeadPlaneMirrored").GetComponent<MeshRenderer>().enabled = false;
        abdomenStandardPlane.transform.Find("AbdomenPlaneMirrored").GetComponent<MeshRenderer>().enabled = false;
        femurStandardPlane.transform.Find("FemurPlaneMirrored").GetComponent<MeshRenderer>().enabled = false;

        //babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_1").GetComponent<LineRenderer>().enabled = false;
        //babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_2").GetComponent<LineRenderer>().enabled = false;
        //babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_3").GetComponent<LineRenderer>().enabled = false;

        headStandardPlane.transform.Find("HeadLabelForUsImage").GetComponent<MeshRenderer>().enabled = false;
        abdomenStandardPlane.transform.Find("AbdomenLabelForUsImage").GetComponent<MeshRenderer>().enabled = false;
        femurStandardPlane.transform.Find("FemurLabelForUsImage").GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
