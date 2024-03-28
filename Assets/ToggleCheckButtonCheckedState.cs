using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;

public class ToggleCheckButtonCheckedState : MonoBehaviour
{
    private bool dataRecordingAlreadyStopped = false;

    public bool isChecked;
    public DataCollection dataCollection;
    public Interactable checkbox;

    // only for visual debugging in unity.
    private bool dataCollectionStopped = false;

    // Start is called before the first frame update
    void Start()
    {
        checkbox = gameObject.GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (checkbox.IsToggled == true)
        {
            isChecked = true;

            if (!dataRecordingAlreadyStopped)
            {
                dataCollection.EndDataRecording();
                dataRecordingAlreadyStopped = true;
                dataCollectionStopped = true;
            }
        }
    }
}
