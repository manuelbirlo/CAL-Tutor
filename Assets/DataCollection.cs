using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;
using System.Globalization;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;

public class DataCollection : MonoBehaviour
{
    public List<UserInteractionData> locationData;
    private float time = 0.0f;
    public float interpolationPeriod = 0.1f;
    public GameObject clariusGameObject;
    public Vector3 hitPos;
    private bool IsDataRecordingOn = false;
    public int TargetUltrasoundPlaneNumber;
    private string reportName;
    private HandTracking handtrackingScript;

    public string PersistentDataPath;
    public string nameOfHitObject;

    private Dictionary<string, Interactable> userSkillLevelButtons;

    private Dictionary<int, string> standardPlanes = new Dictionary<int, string>
    {
        { 1, "Head" },
        { 2, "Abdomen" },
        { 3, "Femur" }
    };

    // Start is called before the first frame update
    void Start()
    {
        PersistentDataPath = Application.persistentDataPath;

        locationData = new List<UserInteractionData>();

        handtrackingScript = GameObject.Find("HandTrackingController").GetComponent<HandTracking>();
    }
     
    public void StartDataRecording()
    {
        string userSkillLevel = string.Empty;

        userSkillLevelButtons = new Dictionary<string, Interactable>
        {
            { "Novice", GameObject.Find("NoviceSkillLevel").GetComponent<Interactable>()},
            { "Intermediate", GameObject.Find("IntermediateSkillLevel").GetComponent<Interactable>()},
            { "Expert", GameObject.Find("ExpertSkillLevel").GetComponent<Interactable>()},
        };

        var values = userSkillLevelButtons.Keys;

        if (userSkillLevelButtons.Values.Count(interactible => interactible.IsToggled) == 1)
        {
            userSkillLevel = userSkillLevelButtons.First(interactible => interactible.Value.IsToggled).Key;
        }
        
        var currentTimeStamp = System.DateTime.UtcNow.ToString("yyyy-MM-dd_HH-mm-ss");

        reportName = userSkillLevel + "_user_dateTime_" + currentTimeStamp + "_standardPlane_" + standardPlanes[TargetUltrasoundPlaneNumber] + "_userData.csv";

        locationData = new List<UserInteractionData>();
        IsDataRecordingOn = true;
    }

    public void EndDataRecording()
    {
        IsDataRecordingOn = false;
        CsvManager.AppendToReport(locationData, reportName);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDataRecordingOn) 
        {
            DateTime utc = DateTime.UtcNow;

            
            var eyeGazeProvider = CoreServices.InputSystem?.EyeGazeProvider;

            if (eyeGazeProvider != null)
            {
                hitPos = eyeGazeProvider.HitPosition;

                var hitInfo = eyeGazeProvider.HitInfo;
               
                if (hitInfo.collider != null)
                {
                    nameOfHitObject = eyeGazeProvider.HitInfo.collider.gameObject.name;
                }
            }

            locationData.Add(new UserInteractionData
            {
                Time = utc.ToString(),
                Milliseconds = utc.Millisecond,
                ClariusPosX = clariusGameObject.transform.position.x,
                ClariusPosY = clariusGameObject.transform.position.y,
                ClariusPosZ = clariusGameObject.transform.position.z,
                ClariusRotationX = clariusGameObject.transform.rotation.x,
                ClariusRotationY = clariusGameObject.transform.rotation.y,
                ClariusRotationZ = clariusGameObject.transform.rotation.z,
                EyeGazeHitPosUSPlaneX = eyeGazeProvider.HitPosition.x,
                EyeGazeHitPosUSPlaneY = eyeGazeProvider.HitPosition.y,
                EyeGazeHitPosUSPlaneZ = eyeGazeProvider.HitPosition.z,
                EyeGazeHitGameObject = nameOfHitObject,
                PalmPosition = handtrackingScript.handPalmPosition,
                WristPosition = handtrackingScript.handWristPosition,
                HeadPos = Camera.main.transform.position,
                HeadRotation = Camera.main.transform.rotation
            });
        }
    }

   
    public void SaveData()
    {
        CsvManager.AppendToReport(locationData, reportName);
    }
}
