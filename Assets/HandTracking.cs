using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;

public class HandTracking : MonoBehaviour
{
    //public GameObject sphere;
    //public bool dataCollectionEnabled = false;
    public Vector3 handPalmPosition;
    public Vector3 handWristPosition;

    //Dictionary<TrackedHandJoint, GameObject> fingerGameObjects = new Dictionary<TrackedHandJoint, GameObject>();

    List<TrackedHandJoint> handJointNames = new List<TrackedHandJoint>
    {
        //TrackedHandJoint.ThumbDistalJoint,
        //TrackedHandJoint.ThumbMetacarpalJoint,
        //TrackedHandJoint.ThumbProximalJoint,
        //TrackedHandJoint.ThumbTip,
        //TrackedHandJoint.IndexDistalJoint,
        //TrackedHandJoint.IndexKnuckle,
        //TrackedHandJoint.IndexMetacarpal,
        //TrackedHandJoint.IndexMiddleJoint,
        //TrackedHandJoint.IndexTip,
        //TrackedHandJoint.MiddleDistalJoint,
        //TrackedHandJoint.MiddleKnuckle,
        //TrackedHandJoint.MiddleMetacarpal,
        //TrackedHandJoint.MiddleMiddleJoint,
        //TrackedHandJoint.MiddleTip,
        //TrackedHandJoint.RingDistalJoint,
        //TrackedHandJoint.RingKnuckle,
        //TrackedHandJoint.RingMetacarpal,
        //TrackedHandJoint.RingMiddleJoint,
        //TrackedHandJoint.RingTip,
        //TrackedHandJoint.PinkyDistalJoint,
        //TrackedHandJoint.PinkyKnuckle,
        //TrackedHandJoint.PinkyMetacarpal,
        //TrackedHandJoint.PinkyMiddleJoint,
        //TrackedHandJoint.PinkyTip, 
        TrackedHandJoint.Palm,
        TrackedHandJoint.Wrist
    };

    private int numberOfJoints; 

    //MixedRealityPose jointPose;
   
    // Start is called before the first frame update
    void Start()
    {
        numberOfJoints = handJointNames.Count;

        // TODO: active this code if tracked hand joint spheres shall be visible
        //for (int i = 0; i < numberOfJoints; i++)
        //{
        //    fingerGameObjects[handJointNames[i]] = Instantiate(sphere, this.transform);
        //}
    }
 
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < numberOfJoints; i++)
        {
            var currentJointName = handJointNames[i];
            

            //var currentHandJointGameObject = fingerGameObjects[currentJointName];
            //currentHandJointGameObject.GetComponent<Renderer>().enabled = false;

            if (HandJointUtils.TryGetJointPose(currentJointName, Handedness.Right, out var jointPose))
            {
                // TODO: active this code if tracked hand joint spheres shall be visible
                //currentHandJointGameObject.GetComponent<Renderer>().enabled = true;
                //currentHandJointGameObject.transform.position = jointPose.Position;

                if (currentJointName == TrackedHandJoint.Palm)
                {
                    handPalmPosition = jointPose.Position;
                }
                else if (currentJointName == TrackedHandJoint.Wrist)
                {
                    handWristPosition = jointPose.Position;
                }
            }
        }
    }
}
