using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddChildObjectsToParent : MonoBehaviour
{
    public GameObject[] createdPlanes;
    public GameObject[] createdProbeSidedLines;
    
    public List<Vector3> CornerVerticesProbePlane;
    public List<Vector3> LocalVerticesProbePlane;
    public List<Vector3> GlobalVerticesProbePlane;

    List<int> CornerIDs = new List<int> { 0, 10, 110, 120 };

    private bool lineRenderingEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lineRenderingEnabled)
        {
            for (int i = 0; i < createdPlanes.Length; i++)
            {
                var currentLine = createdProbeSidedLines[i];
                var currentPlane = createdPlanes[i];

                var lineRenderer = currentLine.GetComponent<LineRenderer>();

                GetVertices(currentPlane);

                lineRenderer.SetPosition(0, CornerVerticesProbePlane[0]);
                lineRenderer.SetPosition(1, CornerVerticesProbePlane[1]);
            }
            
        }
    }

    public void SetParent()
    {
        createdPlanes = GameObject.FindGameObjectsWithTag("USPlane");
        createdProbeSidedLines = GameObject.FindGameObjectsWithTag("ProbeSidedLine");

        foreach (var plane in createdPlanes)
        {
            plane.transform.parent = gameObject.transform;
        }

        //foreach (var line in createdProbeSidedLines)
        //{
        //    line.transform.parent = gameObject.transform;
        //}

        lineRenderingEnabled = true;
    }

    public void DetachFromParent()
    {
        lineRenderingEnabled = false;

        var createdPlanes = GameObject.FindGameObjectsWithTag("USPlane");
        //var createdProbeSidedLines = GameObject.FindGameObjectsWithTag("ProbeSidedLine");

        foreach (var plane in createdPlanes)
        {
            plane.transform.parent = null;
        }

        //foreach (var line in createdProbeSidedLines)
        //{
        //    line.transform.parent = null;
        //}
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
