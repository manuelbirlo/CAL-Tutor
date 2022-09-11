using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class DisplayDataToTextField : MonoBehaviour
{
    //public GameObject textField;
    private Stopwatch stopWatch;
    private PlaneDistanceInformation planeDistanceInformation;
    private PlaneNavigation planeNavigation;

    public string xDifference;
    public string yDifference;
    public string zDifference;

    public string xRotationDifference;
    public string yRotationDifference;
    public string zRotationDifference;

    public string elapsedTime;

    //public TextMeshPro textMesh;

    public GameObject xDifferenceTextField;
    public GameObject yDifferenceTextField;
    public GameObject zDifferenceTextField;

    public GameObject xRotationDifferenceTextField;
    public GameObject yRotationDifferenceTextField;
    public GameObject zRotationDifferenceTextField;

    public GameObject elapsedTimeTextField;

    public GameObject targetPlaneNavigationButton;

    // Start is called before the first frame update
    void OnEnable()
    {
        stopWatch = new Stopwatch();
        stopWatch.Start();

        planeDistanceInformation = gameObject.GetComponent<PlaneDistanceInformation>();
        //planeNavigation = gameObject.GetComponent<PlaneNavigation>();
        var targetPlaneNavigationButtons = GameObject.FindGameObjectsWithTag("NavigateToTargetPlaneButton");

        foreach (var button in targetPlaneNavigationButtons)
        {
            var planeNavigationComponent = button.GetComponent<PlaneNavigation>();

            if (planeNavigationComponent.targetUltrasoundPlane != null)
            {
                planeNavigation = planeNavigationComponent;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // --- Only for debugging -------------------------------------------------
        xDifference = planeDistanceInformation.xDifference.ToString("0.00");
        yDifference = planeDistanceInformation.yDifference.ToString("0.00");
        zDifference = planeDistanceInformation.zDifference.ToString("0.00");

        xRotationDifference = planeDistanceInformation.xRotationDifference.ToString("0.00");
        yRotationDifference = planeDistanceInformation.yRotationDifference.ToString("0.00");
        zRotationDifference = planeDistanceInformation.zRotationDifference.ToString("0.00");
        // -----------------------------------------------------------------------

        //elapsedTime = targetPlaneNavigationButton.GetComponent<PlaneNavigation>().elapsedTime;
        //elapsedTime = planeNavigation.elapsedTime;

        //gameObject.GetComponent<TextMeshPro>().text = planeDistanceInformation.xDifference.ToString("0.00");
        xDifferenceTextField.GetComponent<TextMeshProUGUI>().text = planeDistanceInformation.xDifference.ToString("0.00");
        yDifferenceTextField.GetComponent<TextMeshProUGUI>().text = planeDistanceInformation.yDifference.ToString("0.00");
        zDifferenceTextField.GetComponent<TextMeshProUGUI>().text = planeDistanceInformation.zDifference.ToString("0.00");

        xRotationDifferenceTextField.GetComponent<TextMeshProUGUI>().text = planeDistanceInformation.xRotationDifference.ToString("0.00");
        yRotationDifferenceTextField.GetComponent<TextMeshProUGUI>().text = planeDistanceInformation.yRotationDifference.ToString("0.00");
        zRotationDifferenceTextField.GetComponent<TextMeshProUGUI>().text = planeDistanceInformation.zRotationDifference.ToString("0.00");

        TimeSpan timeSpan = stopWatch.Elapsed;
        elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10);

        elapsedTimeTextField.GetComponent<TextMeshProUGUI>().text = elapsedTime;
    }
}
