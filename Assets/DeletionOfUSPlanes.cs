using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletionOfUSPlanes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public void DeletePlanes()
    {
        var createdPlanes = GameObject.FindGameObjectsWithTag("USPlane");
        var createdProbeSidedLines = GameObject.FindGameObjectsWithTag("ProbeSidedLine");

        // Reset the CloneGameObjects script's imageCounter so that newly created USPlanes start with 1 again.
        var planeGameObject = GameObject.Find("Plane");
        var cloneGameObjectsScript = planeGameObject.GetComponent<CloneGameObjects>();
        cloneGameObjectsScript.imageCounter = 1;

        foreach (var plane in createdPlanes)
        {
            Destroy(plane);
        }

        foreach (var line in createdProbeSidedLines)
        {
            Destroy(line);
        }
    }
}
