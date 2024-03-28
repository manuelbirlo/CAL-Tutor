using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlaneDistanceInformation : MonoBehaviour
{
    public GameObject probePlane;
    public GameObject targetPlane;

    public GameObject chevron1;
    public GameObject chevron2;
    public GameObject chevron3;

    public GameObject headPlane;
    public GameObject abdomenPlane;
    public GameObject femurPlane;

    public int PlaneNumber;

    public float xDifference;
    public float yDifference;
    public float zDifference;

    public float xRotationDifference;
    public float yRotationDifference;
    public float zRotationDifference;
    public  bool Exists;

    public GameObject babyModel;

    // Start is called before the first frame update
    void OnEnable()
    {
        babyModel = GameObject.Find("BabyModel");
        chevron1 = GameObject.Find("ChevronTargetPlane1");
        chevron2 = GameObject.Find("ChevronTargetPlane2");
        chevron3 = GameObject.Find("ChevronTargetPlane3");
       
        if (chevron1 != null)
        {
            Exists = true;
            targetPlane = babyModel.transform.Find("HeadPlane").gameObject;
            
            if (targetPlane == null)
            {
                targetPlane = GameObject.Find("USPlane1");
            }
        }
        else if (chevron2 != null)
        {
            targetPlane = babyModel.transform.Find("AbdomenPlane").gameObject;

            if (targetPlane == null)
            {
                targetPlane = GameObject.Find("USPlane2");
            }
        }
        else if (chevron3 != null)
        {
            targetPlane = babyModel.transform.Find("FemurPlane").gameObject;

            if (targetPlane == null)
            {
                targetPlane = GameObject.Find("USPlane3");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        xDifference = targetPlane.transform.position.x - probePlane.transform.position.x;
        yDifference = targetPlane.transform.position.y - probePlane.transform.position.y;
        zDifference = targetPlane.transform.position.z - probePlane.transform.position.z;

        xRotationDifference = targetPlane.transform.rotation.x - probePlane.transform.rotation.x;
        yRotationDifference = targetPlane.transform.rotation.y - probePlane.transform.rotation.y;
        zRotationDifference = targetPlane.transform.rotation.z - probePlane.transform.rotation.z;

    }
}
