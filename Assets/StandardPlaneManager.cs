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

    public bool loadingOfGameObjectHasFinished = false;

    public List<Vector3> CornerVerticesProbePlane;
    List<Vector3> LocalVerticesProbePlane;
    List<Vector3> GlobalVerticesProbePlane;
    GameObject probeSidedEdgeLineOfUsImage;

    public bool EnteredCase1 = false;
    public bool EnteredCase2 = false;
    public bool EnteredCase3 = false;

    public bool MeshRendererEnabled = false;

    public MeshRenderer rend;

    private GameObject currentProbeSidedLane;
    private GameObject currentPlane;
    private bool lineRenderingEnabled = false;

    List<int> CornerIDs = new List<int> { 0, 10, 110, 120 };

    // Start is called before the first frame update
    void Start()
    {
        babyModel = GameObject.Find("BabyModel");
        csvReaderScript = GameObject.Find("Controller").GetComponent<CsvReader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lineRenderingEnabled)
        {
            var lineRenderer = currentProbeSidedLane.GetComponent<LineRenderer>();

            GetVertices(currentPlane);

            lineRenderer.SetPosition(0, CornerVerticesProbePlane[0]);
            lineRenderer.SetPosition(1, CornerVerticesProbePlane[1]);
        }
    }

    //public void TurnOffAllStandardPlanes()
    //{
    //    headStandardPlane = GameObject.Find("HeadPlane");
    //    abdomenStandardPlane = GameObject.Find("AbdomenPlane");
    //    femurStandardPlane = GameObject.Find("FemurPlane");

    //   // csvReaderScript.DeactivateLineRendering();

    //    headStandardPlane.GetComponent<MeshRenderer>().enabled = false;
    //    abdomenStandardPlane.GetComponent<MeshRenderer>().enabled = false;
    //    femurStandardPlane.GetComponent<MeshRenderer>().enabled = false;

    //    headStandardPlane.transform.Find("HeadPlaneMirrored").GetComponent<MeshRenderer>().enabled = false;
    //    abdomenStandardPlane.transform.Find("AbdomenPlaneMirrored").GetComponent<MeshRenderer>().enabled = false;
    //    femurStandardPlane.transform.Find("FemurPlaneMirrored").GetComponent<MeshRenderer>().enabled = false;

    //    //babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_1").GetComponent<LineRenderer>().enabled = false;
    //    //babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_2").GetComponent<LineRenderer>().enabled = false;
    //    //babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_3").GetComponent<LineRenderer>().enabled = false;

    //    headStandardPlane.transform.Find("HeadLabelForUsImage").GetComponent<MeshRenderer>().enabled = false;
    //    abdomenStandardPlane.transform.Find("AbdomenLabelForUsImage").GetComponent<MeshRenderer>().enabled = false;
    //    femurStandardPlane.transform.Find("FemurLabelForUsImage").GetComponent<MeshRenderer>().enabled = false;
    //}

    public void TurnOnActiveStandardPlaneForNavigation()
    {
        lineRenderingEnabled = true;

        headStandardPlane = GameObject.Find("HeadPlane");
        abdomenStandardPlane = GameObject.Find("AbdomenPlane");
        femurStandardPlane = GameObject.Find("FemurPlane");

        //csvReaderScript.ReactivateLineRendering();
       
        if (activeStandardPlane == 1)
        {
            currentPlane = headStandardPlane;
           
            EnteredCase1 = true;

            rend = headStandardPlane.GetComponent<MeshRenderer>();
            headStandardPlane.GetComponent<MeshRenderer>().enabled = true;

            MeshRendererEnabled = headStandardPlane.GetComponent<MeshRenderer>().enabled;

            headStandardPlane.transform.Find("HeadPlaneMirrored").GetComponent<MeshRenderer>().enabled = true;
            
            //babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_1").GetComponent<LineRenderer>().enabled = true;
            
            headStandardPlane.transform.Find("HeadLabelForUsImage").GetComponent<MeshRenderer>().enabled = true;

            CreateProbeSidedEdgeLinesForAllStandardPlanes(headStandardPlane, 1);
            currentProbeSidedLane = babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_1").gameObject;

        }
        else if (activeStandardPlane == 2)
        {
            currentPlane = abdomenStandardPlane;
            
            EnteredCase2 = true;
            abdomenStandardPlane.GetComponent<MeshRenderer>().enabled = true;

            abdomenStandardPlane.transform.Find("AbdomenPlaneMirrored").GetComponent<MeshRenderer>().enabled = true;

            //babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_2").GetComponent<LineRenderer>().enabled = true;

            abdomenStandardPlane.transform.Find("AbdomenLabelForUsImage").GetComponent<MeshRenderer>().enabled = true;

            CreateProbeSidedEdgeLinesForAllStandardPlanes(abdomenStandardPlane, 2);
            currentProbeSidedLane = babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_2").gameObject;
        }
        else if (activeStandardPlane == 3)
        {
            currentPlane = femurStandardPlane;
            

            EnteredCase3 = true;
            femurStandardPlane.GetComponent<MeshRenderer>().enabled = true;

            femurStandardPlane.transform.Find("FemurPlaneMirrored").GetComponent<MeshRenderer>().enabled = true;

            //babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_2").GetComponent<LineRenderer>().enabled = true;

            femurStandardPlane.transform.Find("FemurLabelForUsImage").GetComponent<MeshRenderer>().enabled = true;

            CreateProbeSidedEdgeLinesForAllStandardPlanes(femurStandardPlane, 3);
            currentProbeSidedLane = babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_3").gameObject;
        }
    }

    public void DeactivateActiveStandardPlane()
    {
        lineRenderingEnabled = false;

        headStandardPlane = GameObject.Find("HeadPlane");
        abdomenStandardPlane = GameObject.Find("AbdomenPlane");
        femurStandardPlane = GameObject.Find("FemurPlane");

        if (activeStandardPlane == 1)
        {
            headStandardPlane.GetComponent<MeshRenderer>().enabled = false;
            
            headStandardPlane.transform.Find("HeadPlaneMirrored").GetComponent<MeshRenderer>().enabled = false;
            
            headStandardPlane.transform.Find("HeadLabelForUsImage").GetComponent<MeshRenderer>().enabled = false;

            babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_1").GetComponent<LineRenderer>().enabled = false;

        }
        else if (activeStandardPlane == 2)
        {
            abdomenStandardPlane.GetComponent<MeshRenderer>().enabled = false;

            abdomenStandardPlane.transform.Find("AbdomenPlaneMirrored").GetComponent<MeshRenderer>().enabled = false;

            abdomenStandardPlane.transform.Find("AbdomenLabelForUsImage").GetComponent<MeshRenderer>().enabled = false;

            babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_2").GetComponent<LineRenderer>().enabled = false;
        }
        else if (activeStandardPlane == 3)
        {
            femurStandardPlane.GetComponent<MeshRenderer>().enabled = false;

            femurStandardPlane.transform.Find("FemurPlaneMirrored").GetComponent<MeshRenderer>().enabled = false;

            femurStandardPlane.transform.Find("FemurLabelForUsImage").GetComponent<MeshRenderer>().enabled = false;

            babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_3").GetComponent<LineRenderer>().enabled = false;
        }
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

            //babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_2").GetComponent<LineRenderer>().enabled = false;
            //babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_3").GetComponent<LineRenderer>().enabled = false;

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

    void CreateProbeSidedEdgeLinesForAllStandardPlanes(GameObject standardPlane, int identifier)
    {
        // ----------- Creation of probe sided edge Line ------------------------------------------------------------------------------
        var probeSidedEdgeLineOfUsImage = new GameObject();
        probeSidedEdgeLineOfUsImage.name = "ProbeSidedEdgeLine_CloneUSPlane_" + identifier;
        probeSidedEdgeLineOfUsImage.tag = "ProbeSidedLine";
        probeSidedEdgeLineOfUsImage.AddComponent<MeshFilter>();

        probeSidedEdgeLineOfUsImage.AddComponent<MeshRenderer>();

        LineRenderer probeSidedEdgeOfUSPlane = probeSidedEdgeLineOfUsImage.AddComponent<LineRenderer>();

        probeSidedEdgeOfUSPlane.transform.parent = babyModel.transform;

        probeSidedEdgeOfUSPlane.gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        probeSidedEdgeOfUSPlane.SetWidth(0.01F, 0.01F);
        probeSidedEdgeOfUSPlane.SetVertexCount(2);

        CornerVerticesProbePlane = new List<Vector3>();
        LocalVerticesProbePlane = new List<Vector3>();
        GlobalVerticesProbePlane = new List<Vector3>();

        GetVertices(standardPlane);
        probeSidedEdgeOfUSPlane.SetPosition(0, CornerVerticesProbePlane[0]);
        probeSidedEdgeOfUSPlane.SetPosition(1, CornerVerticesProbePlane[1]);
    }

    void GetVertices(GameObject planeObject)
    {
        // Probe plane
        LocalVerticesProbePlane = new List<Vector3>(planeObject.GetComponent<MeshFilter>().mesh.vertices);
        GlobalVerticesProbePlane.Clear();
        CornerVerticesProbePlane.Clear();

        foreach (Vector3 point in LocalVerticesProbePlane)
        {
            GlobalVerticesProbePlane.Add(planeObject.transform.TransformPoint(point));
        }

        foreach (int id in CornerIDs)
        {
            CornerVerticesProbePlane.Add(GlobalVerticesProbePlane[id]);
        }
    }
}
