using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;

public class GameObjectInteractibleController : MonoBehaviour
{
    public GameObject toggleButton;
    public GameObject targetGameObject;
    public bool isToggled;

    public void MaintainOriginalObjectManipulationState()
    {
        Interactable interactableComponent = toggleButton.GetComponent<Interactable>();
        isToggled = interactableComponent.IsToggled;

        if (!interactableComponent.IsToggled)
        {
            targetGameObject.GetComponent<BoundsControl>().enabled = false;
            targetGameObject.GetComponent<ObjectManipulator>().enabled = false;
        }
        else
        {
            targetGameObject.GetComponent<BoundsControl>().enabled = true;
            targetGameObject.GetComponent<ObjectManipulator>().enabled = true;
        }
        //var boundsControl = interactibleGameObject.GetComponent<BoundsControl>();
        //var objectManipulator = interactibleGameObject.GetComponent<ObjectManipulator>();

        //if (boundsControl != null)
        //{
        //    if (!boundsControl.enabled)
        //    {
        //        boundsControl.enabled = false;
        //    }
        //}
    }
}
