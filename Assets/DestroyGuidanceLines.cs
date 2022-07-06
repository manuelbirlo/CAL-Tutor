using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGuidanceLines : MonoBehaviour
{
    //public int PlaneNumber;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DestroyArrows()
    {
        GameObject[] allArrows = GameObject.FindGameObjectsWithTag("Arrow");
        GameObject[] allLines = GameObject.FindGameObjectsWithTag("Line");

        foreach (GameObject arrow in allArrows)
        {
            GameObject.Destroy(arrow);
        }

        foreach (GameObject line in allLines)
        {
            GameObject.Destroy(line);
        }
    }
}
