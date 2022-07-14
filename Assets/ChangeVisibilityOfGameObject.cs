using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;

public class ChangeVisibilityOfGameObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private bool indictorIsDeactivated = false;

    // Update is called once per frame
    void Update()
    {
    }

    public GameObject headIndicator;
    public GameObject abdomenIndicator;
    public GameObject femurIndicator;

    public GameObject indicator;
    public bool indicatorExists;

    public void ActivateGameObject()
    {
        if (GameObject.Find("1st_TargetPlaneReached_ToggleCheckBox_32x32") != null)
        {
            headIndicator.SetActive(true);
        }
        else if (GameObject.Find("2nd_TargetPlaneReached_ToggleCheckBox_32x32") != null)
        {
            abdomenIndicator.SetActive(true);
        }
        else if (GameObject.Find("3rd_TargetPlaneReached_ToggleCheckBox_32x32") != null)
        {
            femurIndicator.SetActive(true);
        }
    }

    public void DeactivateGameObject()
    {
        var deactivateMrGuidanceToggleButton = GameObject.Find("DeactivateMRGuidance").GetComponent<Interactable>();

        if (deactivateMrGuidanceToggleButton.IsToggled)
        {
            if (headIndicator.activeSelf)
            {
                headIndicator.SetActive(false);
            }
            else if (abdomenIndicator.activeSelf)
            {
                abdomenIndicator.SetActive(false);
            }
            else if (femurIndicator.activeSelf)
            {
                femurIndicator.SetActive(false);
            }
        }
    }

    public void MakeAllGuidanceDataInvisible()
    {
        var deactivateMrGuidanceToggleButton = GameObject.Find("DeactivateMRGuidance").GetComponent<Interactable>();

        if (deactivateMrGuidanceToggleButton.IsToggled)
        {
            DeactivateDirectionalIndicator();

            MakeArrowsInvisible();

            var cube = GameObject.Find("Cube");
            SetTargetInvisible(cube);

            var usPlane = GameObject.Find("Plane");
            SetTargetInvisible(usPlane);

            var coordinates = GameObject.Find("Coordinates");
            SetTargetInvisible(cube);

            var babyModel = GameObject.Find("BabyModel");
            SetTargetInvisible(babyModel);

            var probe = GameObject.Find("GE_Voluson_2D_aligned");
            SetTargetInvisible(probe);

            var dataScreen = GameObject.Find("ProbeToTargetPlaneDistanceData");
            dataScreen.SetActive(false);

            DeactivePinnedUSPlaneIfPresent();  
        }
 
    }

    public void MakeInvisible()
    {
        this.GetComponent<Renderer>().enabled = false;
    }

    public void MakeArrowsInvisible()
    {
        var arrow1 = GameObject.Find("arrow1");
        var arrow2 = GameObject.Find("arrow2");
        var arrow3 = GameObject.Find("arrow3");
        var arrow4 = GameObject.Find("arrow4");

        var line1 = GameObject.Find("Line1");
        var line2 = GameObject.Find("Line2");
        var line3 = GameObject.Find("Line3");
        var line4 = GameObject.Find("Line4");

        if (arrow1 != null && arrow2 != null && arrow3 != null && arrow4 != null
            && line1 != null && line2 != null && line2 != null && line2 != null)
        {
            arrow1.GetComponent<LineRenderer>().enabled = false;
            arrow2.GetComponent<LineRenderer>().enabled = false;
            arrow3.GetComponent<LineRenderer>().enabled = false;
            arrow4.GetComponent<LineRenderer>().enabled = false;

            line1.GetComponent<LineRenderer>().enabled = false;
            line2.GetComponent<LineRenderer>().enabled = false;
            line3.GetComponent<LineRenderer>().enabled = false;
            line4.GetComponent<LineRenderer>().enabled = false;
        }
    }

    public void SetTargetInvisible()
    {
        Component[] a = this.GetComponentsInChildren(typeof(MeshRenderer));
        foreach (Component b in a)
        {
            Renderer c = (Renderer)b;
            c.enabled = false;
        }
    }


    public void SetTargetInvisible(GameObject targetGameObject)
    {
        Component[] a = targetGameObject.GetComponentsInChildren(typeof(MeshRenderer));
        foreach (Component b in a)
        {
            Renderer c = (Renderer)b;
            c.enabled = false;
        }
    }

    public void SetTargetVisible(GameObject targetGameObject)
    {
        Component[] a = targetGameObject.GetComponentsInChildren(typeof(MeshRenderer));
        foreach (Component b in a)
        {
            Renderer c = (Renderer)b;
            c.enabled = true;
        }
    }

    public void SetTargetVisible()
    {
        Component[] a = this.GetComponentsInChildren(typeof(MeshRenderer));
        foreach (Component b in a)
        {
            Renderer c = (Renderer)b;
            c.enabled = true;
        }
    }

    public void MakeVisible()
    {
        this.GetComponent<Renderer>().enabled = true;
    }

    public void MakeArrowsVisible()
    {
        var arrow1 = GameObject.Find("arrow1");
        var arrow2 = GameObject.Find("arrow2");
        var arrow3 = GameObject.Find("arrow3");
        var arrow4 = GameObject.Find("arrow4");

        var line1 = GameObject.Find("Line1");
        var line2 = GameObject.Find("Line2");
        var line3 = GameObject.Find("Line3");
        var line4 = GameObject.Find("Line4");

        if (arrow1 != null && arrow2 != null && arrow3 != null && arrow4 != null
            && line1 != null && line2 != null && line2 != null && line2 != null)
        {
            arrow1.GetComponent<LineRenderer>().enabled = true;
            arrow2.GetComponent<LineRenderer>().enabled = true;
            arrow3.GetComponent<LineRenderer>().enabled = true;
            arrow4.GetComponent<LineRenderer>().enabled = true;

            line1.GetComponent<LineRenderer>().enabled = true;
            line2.GetComponent<LineRenderer>().enabled = true;
            line3.GetComponent<LineRenderer>().enabled = true;
            line4.GetComponent<LineRenderer>().enabled = true;

        }
    }

    public void ActivateGameObjectIfNotInNavigationMode()
    {
        var arrow1 = GameObject.Find("arrow1");
        var arrow2 = GameObject.Find("arrow2");
        var arrow3 = GameObject.Find("arrow3");
        var arrow4 = GameObject.Find("arrow4");
        
        if (arrow1 != null && arrow2 != null && arrow3 != null && arrow4 != null)
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
        }
    }

    public void DeactivateDirectionalIndicator()
    {
        var indicator1 = GameObject.Find("ChevronTargetPlane1"); 
        var indicator2 = GameObject.Find("ChevronTargetPlane2"); 
        var indicator3 = GameObject.Find("ChevronTargetPlane3");

        indicator = indicator1;
        indicatorExists = false;

        if (indicator1 != null)
        {
            indicator1.GetComponent<MeshRenderer>().enabled = false;
            indicatorExists = indicator1.GetComponent<MeshRenderer>().enabled;
        }
        else if (indicator2 != null)
        {
            indicator2.GetComponent<MeshRenderer>().enabled = false;
        }
        else if (indicator3 != null)
        {
            indicator3.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void ActivateDirectionalIndicator()
    {
        var indicator1 = GameObject.Find("ChevronTargetPlane1");
        var indicator2 = GameObject.Find("ChevronTargetPlane2");
        var indicator3 = GameObject.Find("ChevronTargetPlane3");

        if (indicator1 != null && indicator1.GetComponent<MeshRenderer>().enabled == false)
        {
            indicator1.GetComponent<MeshRenderer>().enabled = true;
        }
        else if (indicator2 != null && indicator2.GetComponent<MeshRenderer>().enabled == false)
        {
            indicator2.GetComponent<MeshRenderer>().enabled = true;
        }
        else if (indicator3 != null && indicator3.GetComponent<MeshRenderer>().enabled == false)
        {
            indicator3.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    public void DeactivePinnedUSPlaneIfPresent()
    {
        var usPlane1 = GameObject.Find("USPlane1");
        var usPlane2 = GameObject.Find("USPlane2");
        var usPlane3 = GameObject.Find("USPlane3");

        var probeSidedPlane1 = GameObject.Find("ProbeSidedEdgeLine_CloneUSPlane1");
        var probeSidedPlane2 = GameObject.Find("ProbeSidedEdgeLine_CloneUSPlane2");
        var probeSidedPlane3 = GameObject.Find("ProbeSidedEdgeLine_CloneUSPlane3");

        var headLabelForUSPlane = GameObject.Find("HeadLabelForUsImage");
        var abdomenLabelForUSPlane = GameObject.Find("AbdomenLabelForUsImage");
        var femurLabelForUSPlane = GameObject.Find("FemurLabelForUsImage ");

        if (usPlane1 != null)
        {
            usPlane1.GetComponent<MeshRenderer>().enabled = false;
        }

        if (usPlane2 != null)
        {
            usPlane2.GetComponent<MeshRenderer>().enabled = false;
        }

        if (usPlane3 != null)
        {
            usPlane3.GetComponent<MeshRenderer>().enabled = false;
        }

        if (probeSidedPlane1 != null)
        {
            probeSidedPlane1.GetComponent<LineRenderer>().enabled = false;
        }

        if (probeSidedPlane2 != null)
        {
            probeSidedPlane2.GetComponent<LineRenderer>().enabled = false;
        }

        if (probeSidedPlane3 != null)
        {
            probeSidedPlane3.GetComponent<LineRenderer>().enabled = false;
        }

        if (headLabelForUSPlane != null)
        {
            headLabelForUSPlane.GetComponent<MeshRenderer>().enabled = false;
        }

        if (abdomenLabelForUSPlane != null)
        {
            abdomenLabelForUSPlane.GetComponent<MeshRenderer>().enabled = false;
        }

        if (femurLabelForUSPlane != null)
        {
            femurLabelForUSPlane.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void ActivePinnedUSPlaneIfPresent()
    {
        var usPlane1 = GameObject.Find("USPlane1");
        var usPlane2 = GameObject.Find("USPlane2");
        var usPlane3 = GameObject.Find("USPlane3");

        var probeSidedPlane1 = GameObject.Find("ProbeSidedEdgeLine_CloneUSPlane1");
        var probeSidedPlane2 = GameObject.Find("ProbeSidedEdgeLine_CloneUSPlane2");
        var probeSidedPlane3 = GameObject.Find("ProbeSidedEdgeLine_CloneUSPlane3");

        var headLabelForUSPlane = GameObject.Find("HeadLabelForUsImage");
        var abdomenLabelForUSPlane = GameObject.Find("AbdomenLabelForUsImage");
        var femurLabelForUSPlane = GameObject.Find("FemurLabelForUsImage ");

        if (usPlane1 != null && usPlane1.GetComponent<MeshRenderer>().enabled == false)
        {
            usPlane1.GetComponent<MeshRenderer>().enabled = true;
        }

        if (usPlane2 != null && usPlane2.GetComponent<MeshRenderer>().enabled == false)
        {
            usPlane2.GetComponent<MeshRenderer>().enabled = true;
        }

        if (usPlane3 != null && usPlane3.GetComponent<MeshRenderer>().enabled == false)
        {
            usPlane3.GetComponent<MeshRenderer>().enabled = true;
        }

        if (probeSidedPlane1 != null)
        {
            probeSidedPlane1.GetComponent<LineRenderer>().enabled = true;
        }

        if (probeSidedPlane2 != null)
        {
            probeSidedPlane2.GetComponent<LineRenderer>().enabled = true;
        }

        if (probeSidedPlane3 != null)
        {
            probeSidedPlane3.GetComponent<LineRenderer>().enabled = true;
        }

        if (headLabelForUSPlane != null)
        {
            headLabelForUSPlane.GetComponent<MeshRenderer>().enabled = true;
        }

        if (abdomenLabelForUSPlane != null)
        {
            abdomenLabelForUSPlane.GetComponent<MeshRenderer>().enabled = true;
        }

        if (femurLabelForUSPlane != null)
        {
            femurLabelForUSPlane.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
