using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneDistanceInformation : MonoBehaviour
{
    public GameObject probePlane;
    public GameObject targetPlane;

    public int PlaneNumber;

    public float xDifference;
    public float yDifference;
    public float zDifference;

    public float xRotationDifference;
    public float yRotationDifference;
    public float zRotationDifference;

    // Start is called before the first frame update
    void OnEnable()
    {
        targetPlane = GameObject.Find("USPlane" + PlaneNumber);
    }

    // Update is called once per frame
    void Update()
    {
        //if (targetPlane == null)
        //{
        //    targetPlane = GameObject.Find("USPlane" + PlaneNumber);
        //}

        xDifference = targetPlane.transform.position.x - probePlane.transform.position.x;
        yDifference = targetPlane.transform.position.y - probePlane.transform.position.y;
        zDifference = targetPlane.transform.position.z - probePlane.transform.position.z;

        xRotationDifference = targetPlane.transform.rotation.x - probePlane.transform.rotation.x;
        yRotationDifference = targetPlane.transform.rotation.y - probePlane.transform.rotation.y;
        zRotationDifference = targetPlane.transform.rotation.z - probePlane.transform.rotation.z;
    }
}
