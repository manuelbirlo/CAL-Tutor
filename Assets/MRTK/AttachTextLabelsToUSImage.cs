using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachTextLabelsToUSImage : MonoBehaviour
{
    //public GameObject ultrasoundImage;

    public GameObject textLabel;

    public GameObject foundPlane;

    public string nameOfTargetPlane;

    public Quaternion rotation;
    public Vector3 position;

    public void AttachLabel()
    {
        var originalTextLabel = GameObject.Find("HeadLabelForUsImage");

        rotation = originalTextLabel.transform.rotation;
        position = originalTextLabel.transform.position;

        var ultrasoundImage = GameObject.Find(nameOfTargetPlane);
        foundPlane = ultrasoundImage;

        textLabel.transform.parent = ultrasoundImage.transform;

        textLabel.transform.position = new Vector3(0.1791698f, 0.1134472f, 1.4507f);
        textLabel.transform.rotation = new Quaternion(0f, 0f, 0.03489959f, 0.999391f);
        //textLabel.transform.position = new Vector3(-5.5f, 0f, 8f);
        //textLabel.transform.rotation = new Quaternion(90f, -4f, 176f, 1f);
        //textLabel.transform.localScale = new Vector3(1f, 1f, 1f);
        

    }

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
