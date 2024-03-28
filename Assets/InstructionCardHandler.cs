using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;

public class InstructionCardHandler : MonoBehaviour
{
    public GameObject dislayInstructionCardButton;
    public GameObject instructionCardPlane;
    public string nameOfGameObject;
    public bool isToggled;
    public Interactable checkbox;

    void Start()
    {
        checkbox = dislayInstructionCardButton.GetComponent<Interactable>();
    }

    public void ActivateCard()
    {
        isToggled = checkbox.IsToggled;

        if (checkbox.IsToggled == true)
        {
            nameOfGameObject = gameObject.name;

            if (gameObject.name == "NavigateToHead")
            {
                instructionCardPlane.SetActive(true);
            }
            else if (gameObject.name == "NavigateToAbdomen")
            {
                instructionCardPlane.SetActive(true);

            }
            else if (gameObject.name == "NavigateToFemur")
            {
                instructionCardPlane.SetActive(true);

            }
        }
            
    }
}
