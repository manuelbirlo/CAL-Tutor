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

    public string[] currentSplitLine;

    private Dictionary<string, string[]> parsedLines = new Dictionary<string, string[]>();

    public float HeadRotX;
    public float HeadRotY;
    public float HeadRotZ;

    private Quaternion localRotationAsEulerAngles;

    // Start is called before the first frame update
    void Start()
    {
        
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

        HeadRotX = headStandardPlane.transform.localEulerAngles.x;
        HeadRotY = headStandardPlane.transform.localEulerAngles.y;
        HeadRotZ = headStandardPlane.transform.localEulerAngles.z;

        localRotationAsEulerAngles = Quaternion.Euler(new Vector3(headStandardPlane.transform.localRotation.x, 
            headStandardPlane.transform.localRotation.y,
            headStandardPlane.transform.localRotation.z));

        HeadRotX = localRotationAsEulerAngles.x;
        //using (StreamReader strReader = new StreamReader("Assets/StandardPlanes_and_Cube_Locations - Copy.csv"))
        //{
        //    bool endOfFile = false;

        //    string currentLine;

        //    while (!endOfFile)
        //    {
        //        currentLine = strReader.ReadLine();

        //        if (currentLine == null)
        //        {
        //            endOfFile = true;
        //            break;
        //        }

        //        //dataValues is each row
        //        currentSplitLine = currentLine.Split(',');

        //        if (currentSplitLine[0] == "pos_x")
        //        {
        //            parsedLines["pos_x"] = currentSplitLine;
        //        }
        //        else if (currentSplitLine[0] == "pos_y")
        //        {
        //            parsedLines["pos_y"] = currentSplitLine;
        //        }
        //        else if (currentSplitLine[0] == "pos_z")
        //        {
        //            parsedLines["pos_z"] = currentSplitLine;
        //        }
        //        else if (currentSplitLine[0] == "rot_x")
        //        {
        //            parsedLines["rot_x"] = currentSplitLine;
        //        }
        //        else if (currentSplitLine[0] == "rot_y")
        //        {
        //            parsedLines["rot_y"] = currentSplitLine;
        //        }
        //        else if (currentSplitLine[0] == "rot_z")
        //        {
        //            parsedLines["rot_z"] = currentSplitLine;
        //        }
        //    }
        //}

        using (StreamWriter strWriter = new StreamWriter("Assets/GameObjectLocations.csv"))
        {
            //foreach (var keyValuePair in parsedLines)
            //{
            //    strWriter.WriteLine(keyValuePair.Value);
            //}
               
            var firstLine = ",Head_Plane,Abdomen_Plane,Femur_Plane,Cube_To_Probe";
            strWriter.WriteLine(firstLine);

            var secondLine = string.Format(@"pos_x,{0},{1},{2},{3}", 
                                            headStandardPlane.transform.localPosition.x,
                                            abdomenStandardPlane.transform.localPosition.x,
                                            femurStandardPlane.transform.localPosition.x,
                                            cube.transform.localPosition.x
                                            );

            strWriter.WriteLine(secondLine);

            var thirdLine = string.Format(@"pos_y,{0},{1},{2},{3}",
                                           headStandardPlane.transform.localPosition.y,
                                           abdomenStandardPlane.transform.localPosition.y,
                                           femurStandardPlane.transform.localPosition.y,
                                           cube.transform.localPosition.y
                                           );

            strWriter.WriteLine(thirdLine);

            var fourthLine = string.Format(@"pos_z,{0},{1},{2},{3}",
                                           headStandardPlane.transform.localPosition.z,
                                           abdomenStandardPlane.transform.localPosition.z,
                                           femurStandardPlane.transform.localPosition.z,
                                           cube.transform.localPosition.z
                                           );

            strWriter.WriteLine(fourthLine);

            var fifthLine = string.Format(@"rot_x,{0},{1},{2},{3}",
                                           headStandardPlane.transform.localRotation.x,
                                           abdomenStandardPlane.transform.localRotation.x,
                                           femurStandardPlane.transform.localRotation.x,
                                           cube.transform.localRotation.x 
                                           );

            strWriter.WriteLine(fifthLine);

            var sixthLine = string.Format(@"rot_y,{0},{1},{2},{3}",
                                           headStandardPlane.transform.localRotation.y,
                                           abdomenStandardPlane.transform.localRotation.y,
                                           femurStandardPlane.transform.localRotation.y,
                                           cube.transform.localRotation.y
                                           );

            strWriter.WriteLine(sixthLine);

            var seventhLine = string.Format(@"rot_z,{0},{1},{2},{3}",
                                           headStandardPlane.transform.localRotation.z,
                                           abdomenStandardPlane.transform.localRotation.z,
                                           femurStandardPlane.transform.localRotation.z,
                                           cube.transform.localRotation.z
                                           );

            strWriter.WriteLine(seventhLine);

            var eightLine = string.Format(@"rot_w,{0},{1},{2},{3}",
                                           headStandardPlane.transform.localRotation.w,
                                           abdomenStandardPlane.transform.localRotation.w,
                                           femurStandardPlane.transform.localRotation.w,
                                           cube.transform.localRotation.w
                                           );

            strWriter.WriteLine(eightLine);
        }

    }
}
