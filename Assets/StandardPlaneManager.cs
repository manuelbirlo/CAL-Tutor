using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardPlaneManager : MonoBehaviour
{
    public int activeStandardPlane;
    private GameObject headStandardPlane;
    private GameObject abdomenStandardPlane;
    private GameObject femurStandardPlane;
    private GameObject babyModel;

    private CsvReader csvReaderScript;

    // Start is called before the first frame update
    void Start()
    {
        babyModel = GameObject.Find("BabyModel");
        csvReaderScript = GameObject.Find("Controller").GetComponent<CsvReader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeactivateStandardPlanes()
    {
        headStandardPlane = GameObject.Find("HeadPlane");
        abdomenStandardPlane = GameObject.Find("AbdomenPlane");
        femurStandardPlane = GameObject.Find("FemurPlane");

        csvReaderScript.DeactivateLineRendering();

        if (activeStandardPlane == 1)
        {
            abdomenStandardPlane.GetComponent<MeshRenderer>().enabled = false;
            femurStandardPlane.GetComponent<MeshRenderer>().enabled = false;

            abdomenStandardPlane.transform.Find("AbdomenPlaneMirrored").GetComponent<MeshRenderer>().enabled = false;
            femurStandardPlane.transform.Find("FemurPlaneMirrored").GetComponent<MeshRenderer>().enabled = false;

            babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_2").GetComponent<LineRenderer>().enabled = false;
            babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_3").GetComponent<LineRenderer>().enabled = false;

            abdomenStandardPlane.transform.Find("AbdomenLabelForUsImage").GetComponent<MeshRenderer>().enabled = false;
            femurStandardPlane.transform.Find("FemurLabelForUsImage").GetComponent<MeshRenderer>().enabled = false;
        }
        else if (activeStandardPlane == 2)
        {
            headStandardPlane.GetComponent<MeshRenderer>().enabled = false;
            femurStandardPlane.GetComponent<MeshRenderer>().enabled = false;

            headStandardPlane.transform.Find("HeadPlaneMirrored").GetComponent<MeshRenderer>().enabled = false;
            femurStandardPlane.transform.Find("FemurPlaneMirrored").GetComponent<MeshRenderer>().enabled = false;

            babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_1").GetComponent<LineRenderer>().enabled = false;
            babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_3").GetComponent<LineRenderer>().enabled = false;

            headStandardPlane.transform.Find("HeadLabelForUsImage").GetComponent<MeshRenderer>().enabled = false;
            femurStandardPlane.transform.Find("FemurLabelForUsImage").GetComponent<MeshRenderer>().enabled = false;
        }
        else if (activeStandardPlane == 3)
        {
            headStandardPlane.GetComponent<MeshRenderer>().enabled = false;
            abdomenStandardPlane.GetComponent<MeshRenderer>().enabled = false;

            abdomenStandardPlane.transform.Find("AbdomenPlaneMirrored").GetComponent<MeshRenderer>().enabled = false;
            headStandardPlane.transform.Find("HeadPlaneMirrored").GetComponent<MeshRenderer>().enabled = false;

            babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_1").GetComponent<LineRenderer>().enabled = false;
            babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_2").GetComponent<LineRenderer>().enabled = false;

            headStandardPlane.transform.Find("HeadLabelForUsImage").GetComponent<MeshRenderer>().enabled = false;
            abdomenStandardPlane.transform.Find("AbdomenLabelForUsImage").GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void ReactivateStandardPlanes()
    {
        headStandardPlane = GameObject.Find("HeadPlane");
        abdomenStandardPlane = GameObject.Find("AbdomenPlane");
        femurStandardPlane = GameObject.Find("FemurPlane");

        csvReaderScript.ReactivateLineRendering();

        if (activeStandardPlane == 1)
        {
            abdomenStandardPlane.GetComponent<MeshRenderer>().enabled = true;
            femurStandardPlane.GetComponent<MeshRenderer>().enabled = true;

            abdomenStandardPlane.transform.Find("AbdomenPlaneMirrored").GetComponent<MeshRenderer>().enabled = true;
            femurStandardPlane.transform.Find("FemurPlaneMirrored").GetComponent<MeshRenderer>().enabled = true;

            babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_2").GetComponent<LineRenderer>().enabled = true;
            babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_3").GetComponent<LineRenderer>().enabled = true;

            abdomenStandardPlane.transform.Find("AbdomenLabelForUsImage").GetComponent<MeshRenderer>().enabled = true;
            femurStandardPlane.transform.Find("FemurLabelForUsImage").GetComponent<MeshRenderer>().enabled = true;
        }
        else if (activeStandardPlane == 2)
        {
            headStandardPlane.GetComponent<MeshRenderer>().enabled = true;
            femurStandardPlane.GetComponent<MeshRenderer>().enabled = true;

            headStandardPlane.transform.Find("HeadPlaneMirrored").GetComponent<MeshRenderer>().enabled = true;
            femurStandardPlane.transform.Find("FemurPlaneMirrored").GetComponent<MeshRenderer>().enabled = true;

            babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_1").GetComponent<LineRenderer>().enabled = true;
            babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_3").GetComponent<LineRenderer>().enabled = true;

            headStandardPlane.transform.Find("HeadLabelForUsImage").GetComponent<MeshRenderer>().enabled = true;
            femurStandardPlane.transform.Find("FemurLabelForUsImage").GetComponent<MeshRenderer>().enabled = true;
        }
        else if (activeStandardPlane == 3)
        {
            headStandardPlane.GetComponent<MeshRenderer>().enabled = true;
            abdomenStandardPlane.GetComponent<MeshRenderer>().enabled = true;

            abdomenStandardPlane.transform.Find("AbdomenPlaneMirrored").GetComponent<MeshRenderer>().enabled = true;
            headStandardPlane.transform.Find("HeadPlaneMirrored").GetComponent<MeshRenderer>().enabled = true;

            babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_1").GetComponent<LineRenderer>().enabled = true;
            babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_2").GetComponent<LineRenderer>().enabled = true;

            headStandardPlane.transform.Find("HeadLabelForUsImage").GetComponent<MeshRenderer>().enabled = true;
            abdomenStandardPlane.transform.Find("AbdomenLabelForUsImage").GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
