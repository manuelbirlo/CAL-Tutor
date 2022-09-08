using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbeSidedLanesManager : MonoBehaviour
{
    
    public GameObject checkBox;
    public GameObject babyModel;

    GameObject headStandardPlane;
    GameObject abdomenStandardPlane;
    GameObject femurStandardPlane;

    public List<Vector3> CornerVerticesProbePlane;
    List<Vector3> LocalVerticesProbePlane;
    List<Vector3> GlobalVerticesProbePlane;
    GameObject probeSidedEdgeLineOfUsImage;

    public GameObject probeSidedLane1;
    public GameObject probeSidedLane2;
    public GameObject probeSidedLane3;

    //private Interactable interactableOfCheckBox;
    private GameObject[] createdProbeSidedLines;

    List<int> CornerIDs = new List<int> { 0, 10, 110, 120 };

    // Start is called before the first frame update
    void Start()
    {
        headStandardPlane = GameObject.Find("HeadPlane");
        abdomenStandardPlane = GameObject.Find("AbdomenPlane");
        femurStandardPlane = GameObject.Find("FemurPlane");
    }

    public void ActivateProbeSidedEdges()
    {
        if (babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_1") != null)
        {
            probeSidedLane1 = babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_1").gameObject;
            probeSidedLane1.SetActive(true);
        }

        if (babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_2") != null)
        {
            probeSidedLane2 = babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_2").gameObject;
            probeSidedLane2.SetActive(true);
        }

        if (babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_3") != null)
        {
            probeSidedLane3 = babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_3").gameObject;
            probeSidedLane3.SetActive(true);
        }
        //createdProbeSidedLines = GameObject.FindGameObjectsWithTag("ProbeSidedLine");

        //foreach (var probeSidedLane in createdProbeSidedLines)
        //{
        //    probeSidedLane.SetActive(true);
        //}

        //CreateProbeSidedEdgeLinesForAllStandardPlanes(headStandardPlane, 1);
        //CreateProbeSidedEdgeLinesForAllStandardPlanes(abdomenStandardPlane, 2);
        //CreateProbeSidedEdgeLinesForAllStandardPlanes(femurStandardPlane, 3);
    }

    public void DeactivateProbeSidedEdges()
    {
      
        if (babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_1") != null)
        {
            probeSidedLane1 = babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_1").gameObject;
            probeSidedLane1.SetActive(false);
        }

        if (babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_2") != null)
        {
            probeSidedLane2 = babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_2").gameObject;
            probeSidedLane2.SetActive(false);
        }

        if (babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_3") != null)
        {
            probeSidedLane3 = babyModel.transform.Find("ProbeSidedEdgeLine_CloneUSPlane_3").gameObject;
            probeSidedLane3.SetActive(false);
        }
    }

    //void CreateProbeSidedEdgeLinesForAllStandardPlanes(GameObject standardPlane, int identifier)
    //{
    //    // ----------- Creation of probe sided edge Line ------------------------------------------------------------------------------
    //    var probeSidedEdgeLineOfUsImage = new GameObject();
    //    probeSidedEdgeLineOfUsImage.name = "ProbeSidedEdgeLine_CloneUSPlane_" + identifier;
    //    probeSidedEdgeLineOfUsImage.tag = "ProbeSidedLine";
    //    probeSidedEdgeLineOfUsImage.AddComponent<MeshFilter>();

    //    probeSidedEdgeLineOfUsImage.AddComponent<MeshRenderer>();



    //    LineRenderer probeSidedEdgeOfUSPlane = probeSidedEdgeLineOfUsImage.AddComponent<LineRenderer>();

    //    probeSidedEdgeOfUSPlane.transform.parent = babyModel.transform;

    //    probeSidedEdgeOfUSPlane.gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    //    probeSidedEdgeOfUSPlane.SetWidth(0.01F, 0.01F);
    //    probeSidedEdgeOfUSPlane.SetVertexCount(2);

    //    CornerVerticesProbePlane = new List<Vector3>();
    //    LocalVerticesProbePlane = new List<Vector3>();
    //    GlobalVerticesProbePlane = new List<Vector3>();

    //    GetVertices(standardPlane);
    //    probeSidedEdgeOfUSPlane.SetPosition(0, CornerVerticesProbePlane[0]);
    //    probeSidedEdgeOfUSPlane.SetPosition(1, CornerVerticesProbePlane[1]);
    //}

    //void GetVertices(GameObject planeObject)
    //{
    //    // CurrentGameObject = planeObject;

    //    // Probe plane
    //    LocalVerticesProbePlane = new List<Vector3>(planeObject.GetComponent<MeshFilter>().mesh.vertices);
    //    GlobalVerticesProbePlane.Clear();
    //    CornerVerticesProbePlane.Clear();

    //    foreach (Vector3 point in LocalVerticesProbePlane)
    //    {
    //        GlobalVerticesProbePlane.Add(planeObject.transform.TransformPoint(point));
    //    }

    //    foreach (int id in CornerIDs)
    //    {
    //        CornerVerticesProbePlane.Add(GlobalVerticesProbePlane[id]);
    //    }
    //}
}
