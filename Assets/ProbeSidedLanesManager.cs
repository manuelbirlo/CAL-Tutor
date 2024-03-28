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
}
