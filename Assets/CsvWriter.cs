using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;

public class CsvWriter : MonoBehaviour
{
    public GameObject headStandardPlane;
    GameObject abdomenStandardPlane;
    GameObject femurStandardPlane;
    public GameObject cube;
    public GameObject probe;

    public string[] currentSplitLine;

    private Dictionary<string, string[]> parsedLines = new Dictionary<string, string[]>();

    public float HeadRotX;
    public float HeadRotY;
    public float HeadRotZ;

    private Quaternion localRotationAsEulerAngles;

    private string DataPath; 

    // Start is called before the first frame update
    void Start()
    {
        DataPath = Application.persistentDataPath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCsv()
    {
        headStandardPlane = GameObject.Find("HeadPlane");
        abdomenStandardPlane = GameObject.Find("AbdomenPlane");
        femurStandardPlane = GameObject.Find("FemurPlane");
        cube = GameObject.Find("CubeRelativeToProbe");
        probe = GameObject.Find("GE_Voluson_2D_aligned");

        HeadRotX = headStandardPlane.transform.localEulerAngles.x;
        HeadRotY = headStandardPlane.transform.localEulerAngles.y;
        HeadRotZ = headStandardPlane.transform.localEulerAngles.z;

        localRotationAsEulerAngles = Quaternion.Euler(new Vector3(headStandardPlane.transform.localRotation.x, 
            headStandardPlane.transform.localRotation.y,
            headStandardPlane.transform.localRotation.z));

        HeadRotX = localRotationAsEulerAngles.x;
        
        using (StreamWriter strWriter = new StreamWriter("Assets/GameObjectLocations.csv"))
        {
            var firstLine = ",Head_Plane,Abdomen_Plane,Femur_Plane,Cube_To_Probe";
            strWriter.WriteLine(firstLine);

            var secondLine = string.Format(@"pos_x,{0},{1},{2},{3},{4}", 
                                            headStandardPlane.transform.localPosition.x,
                                            abdomenStandardPlane.transform.localPosition.x,
                                            femurStandardPlane.transform.localPosition.x,
                                            cube.transform.localPosition.x,
                                            probe.transform.localPosition.x
                                            );

            strWriter.WriteLine(secondLine);

            var thirdLine = string.Format(@"pos_y,{0},{1},{2},{3},{4}",
                                           headStandardPlane.transform.localPosition.y,
                                           abdomenStandardPlane.transform.localPosition.y,
                                           femurStandardPlane.transform.localPosition.y,
                                           cube.transform.localPosition.y,
                                           probe.transform.localPosition.y
                                           );

            strWriter.WriteLine(thirdLine);

            var fourthLine = string.Format(@"pos_z,{0},{1},{2},{3},{4}",
                                           headStandardPlane.transform.localPosition.z,
                                           abdomenStandardPlane.transform.localPosition.z,
                                           femurStandardPlane.transform.localPosition.z,
                                           cube.transform.localPosition.z,
                                           probe.transform.localPosition.z
                                           );

            strWriter.WriteLine(fourthLine);

            var fifthLine = string.Format(@"rot_x,{0},{1},{2},{3},{4}",
                                           headStandardPlane.transform.localRotation.x,
                                           abdomenStandardPlane.transform.localRotation.x,
                                           femurStandardPlane.transform.localRotation.x,
                                           cube.transform.localRotation.x,
                                           probe.transform.localRotation.x 
                                           );

            strWriter.WriteLine(fifthLine);

            var sixthLine = string.Format(@"rot_y,{0},{1},{2},{3},{4}",
                                           headStandardPlane.transform.localRotation.y,
                                           abdomenStandardPlane.transform.localRotation.y,
                                           femurStandardPlane.transform.localRotation.y,
                                           cube.transform.localRotation.y,
                                           probe.transform.localRotation.y
                                           );

            strWriter.WriteLine(sixthLine);

            var seventhLine = string.Format(@"rot_z,{0},{1},{2},{3},{4}",
                                           headStandardPlane.transform.localRotation.z,
                                           abdomenStandardPlane.transform.localRotation.z,
                                           femurStandardPlane.transform.localRotation.z,
                                           cube.transform.localRotation.z,
                                           probe.transform.localRotation.z
                                           );

            strWriter.WriteLine(seventhLine);

            var eightLine = string.Format(@"rot_w,{0},{1},{2},{3},{4}",
                                           headStandardPlane.transform.localRotation.w,
                                           abdomenStandardPlane.transform.localRotation.w,
                                           femurStandardPlane.transform.localRotation.w,
                                           cube.transform.localRotation.w,
                                           probe.transform.localRotation.w
                                           );

            strWriter.WriteLine(eightLine);
        }

        // Save the same .csv file in the persistent data path on the device.
        using (StreamWriter strWriter = new StreamWriter(string.Format("{0}/GameObjectLocations.csv", DataPath)))
        {
            var firstLine = ",Head_Plane,Abdomen_Plane,Femur_Plane,Cube_To_Probe";
            strWriter.WriteLine(firstLine);

            var secondLine = string.Format(@"pos_x,{0},{1},{2},{3},{4}",
                                            headStandardPlane.transform.localPosition.x,
                                            abdomenStandardPlane.transform.localPosition.x,
                                            femurStandardPlane.transform.localPosition.x,
                                            cube.transform.localPosition.x,
                                            probe.transform.localPosition.x
                                            );

            strWriter.WriteLine(secondLine);

            var thirdLine = string.Format(@"pos_y,{0},{1},{2},{3},{4}",
                                           headStandardPlane.transform.localPosition.y,
                                           abdomenStandardPlane.transform.localPosition.y,
                                           femurStandardPlane.transform.localPosition.y,
                                           cube.transform.localPosition.y,
                                           probe.transform.localPosition.y
                                           );

            strWriter.WriteLine(thirdLine);

            var fourthLine = string.Format(@"pos_z,{0},{1},{2},{3},{4}",
                                           headStandardPlane.transform.localPosition.z,
                                           abdomenStandardPlane.transform.localPosition.z,
                                           femurStandardPlane.transform.localPosition.z,
                                           cube.transform.localPosition.z,
                                           probe.transform.localPosition.z
                                           );

            strWriter.WriteLine(fourthLine);

            var fifthLine = string.Format(@"rot_x,{0},{1},{2},{3},{4}",
                                           headStandardPlane.transform.localRotation.x,
                                           abdomenStandardPlane.transform.localRotation.x,
                                           femurStandardPlane.transform.localRotation.x,
                                           cube.transform.localRotation.x,
                                           probe.transform.localRotation.x
                                           );

            strWriter.WriteLine(fifthLine);

            var sixthLine = string.Format(@"rot_y,{0},{1},{2},{3},{4}",
                                           headStandardPlane.transform.localRotation.y,
                                           abdomenStandardPlane.transform.localRotation.y,
                                           femurStandardPlane.transform.localRotation.y,
                                           cube.transform.localRotation.y,
                                           probe.transform.localRotation.y
                                           );

            strWriter.WriteLine(sixthLine);

            var seventhLine = string.Format(@"rot_z,{0},{1},{2},{3},{4}",
                                           headStandardPlane.transform.localRotation.z,
                                           abdomenStandardPlane.transform.localRotation.z,
                                           femurStandardPlane.transform.localRotation.z,
                                           cube.transform.localRotation.z,
                                           probe.transform.localRotation.z
                                           );

            strWriter.WriteLine(seventhLine);

            var eightLine = string.Format(@"rot_w,{0},{1},{2},{3},{4}",
                                           headStandardPlane.transform.localRotation.w,
                                           abdomenStandardPlane.transform.localRotation.w,
                                           femurStandardPlane.transform.localRotation.w,
                                           cube.transform.localRotation.w,
                                           probe.transform.localRotation.w
                                           );

            strWriter.WriteLine(eightLine);
        }
    }
}
