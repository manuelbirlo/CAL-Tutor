using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;

public class CsvReader : MonoBehaviour
{
    public GameObject[] createdProbeSidedLines;
    public GameObject[] createdPlanes;

    private static string lineSplitRegex = "@\r\n|\n\r|\n|\r";

    public StandardPlaneLocations standardPlaneLocations;
    public float headPlaneX;
    public float abdomenPlaneX;
    public float femurPlaneX;
           
    public float headPlaneY;
    public float abdomenPlaneY;
    public float femurPlaneY;
           
    public float headPlaneZ;
    public float abdomenPlaneZ;
    public float femurPlaneZ;
           
    public float headPlaneRotX;
    public float abdomenPlaneRotX;
    public float femurPlaneRotX;
           
    public float headPlaneRotY;
    public float abdomenPlaneRotY;
    public float femurPlaneRotY;
           
    public float headPlaneRotZ;
    public float abdomenPlaneRotZ;
    public float femurPlaneRotZ;

    public float cubeX;
    public float cubeY;
    public float cubeZ;
    public float cubeRotX;
    public float cubeRotY;
    public float cubeRotZ;

    private bool lineRenderingEnabled = false;

    private bool disableProbeSidedLanes = true;

    //LineRenderer probeSidedEdgeOfUSPlane;
    public List<Vector3> CornerVerticesProbePlane;
    List<Vector3> LocalVerticesProbePlane;
    List<Vector3> GlobalVerticesProbePlane;
    GameObject probeSidedEdgeLineOfUsImage;

    List<int> CornerIDs = new List<int> { 0, 10, 110, 120 };

    public GameObject headStandardPlane;
    GameObject abdomenStandardPlane;
    GameObject femurStandardPlane;
    public GameObject cube;

    public GameObject babyModel;

    public GameObject checkBox;

    private Interactable interactableOfCheckBox;

    // Start is called before the first frame update
    void Start()
    {
        interactableOfCheckBox = checkBox.GetComponent<Interactable>();

        headStandardPlane = GameObject.Find("HeadPlane");
        abdomenStandardPlane = GameObject.Find("AbdomenPlane");
        femurStandardPlane = GameObject.Find("FemurPlane");
        cube = GameObject.Find("CubeRelativeToProbe");

        ReadCsv();
        PlaceStandardPlanes();
        PlaceCube();

        CreateProbeSidedEdgeLinesForAllStandardPlanes(headStandardPlane, 1);
        CreateProbeSidedEdgeLinesForAllStandardPlanes(abdomenStandardPlane, 2);
        CreateProbeSidedEdgeLinesForAllStandardPlanes(femurStandardPlane, 3);

        createdProbeSidedLines = GameObject.FindGameObjectsWithTag("ProbeSidedLine");
        createdPlanes = GameObject.FindGameObjectsWithTag("StandardPlane");
        lineRenderingEnabled = true;
    }

    void Update()
    {
        if (lineRenderingEnabled && !interactableOfCheckBox.IsToggled)
        {
            for (int i = 0; i < createdProbeSidedLines.Length; i++)
            {
                var currentLine = createdProbeSidedLines[i];
                var currentPlane = createdPlanes[i];

                var lineRenderer = currentLine.GetComponent<LineRenderer>();

                GetVertices(currentPlane);

                lineRenderer.SetPosition(0, CornerVerticesProbePlane[0]);
                lineRenderer.SetPosition(1, CornerVerticesProbePlane[1]);
            }  
        }
    }

    private void ReadCsv()
    {
        TextAsset file = Resources.Load<TextAsset>("StandardPlanes_and_Cube_Locations");
        string[] lines = Regex.Split(file.text, lineSplitRegex);
        headPlaneX = 0;
        abdomenPlaneX = 0f;
        femurPlaneX = 0f;

        headPlaneY = 0f;
        abdomenPlaneY = 0f;
        femurPlaneY = 0f;

        headPlaneZ = 0f;
        abdomenPlaneZ = 0f;
        femurPlaneZ = 0f;

        headPlaneRotX = 0f;
        abdomenPlaneRotX = 0f;
        femurPlaneRotX = 0f;

        headPlaneRotY = 0f;
        abdomenPlaneRotY = 0f;
        femurPlaneRotY = 0f;

        headPlaneRotZ = 0f;
        abdomenPlaneRotZ = 0f;
        femurPlaneRotZ = 0f;

        cubeX = 0f;
        cubeY = 0f;
        cubeZ = 0f;
        cubeRotX = 0f;
        cubeRotY = 0f;
        cubeRotZ = 0f;

        // Skip the header line of the .csv file.
        for (int i = 1; i < lines.Length; i++)
        {
            var currentSplitLine = lines[i].Split(',');

            if (currentSplitLine[0] == "pos_x")
            {
                if (!float.TryParse(currentSplitLine[1].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out headPlaneX))
                {
                    headPlaneX = 0;
                }

                if (!float.TryParse(currentSplitLine[2].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out abdomenPlaneX))
                {
                    abdomenPlaneX = 0;
                }

                if (!float.TryParse(currentSplitLine[3].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out femurPlaneX))
                {
                    femurPlaneX = 0;
                }

                if (!float.TryParse(currentSplitLine[4].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out cubeX))
                {
                    cubeX = 0;
                }
            }
            else if (currentSplitLine[0] == "pos_y")
            {
                if (!float.TryParse(currentSplitLine[1].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out headPlaneY))
                {
                    headPlaneY = 0;
                }

                if (!float.TryParse(currentSplitLine[2].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out abdomenPlaneY))
                {
                    abdomenPlaneY = 0;
                }

                if (!float.TryParse(currentSplitLine[3].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out femurPlaneY))
                {
                    femurPlaneY = 0;
                }

                if (!float.TryParse(currentSplitLine[4].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out cubeY))
                {
                    cubeY = 0;
                }
            }
            else if (currentSplitLine[0] == "pos_z")
            {
                if (!float.TryParse(currentSplitLine[1].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out headPlaneZ))
                {
                    headPlaneZ = 0;
                }

                if (!float.TryParse(currentSplitLine[2].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out abdomenPlaneZ))
                {
                    abdomenPlaneZ = 0;
                }

                if (!float.TryParse(currentSplitLine[3].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out femurPlaneZ))
                {
                    femurPlaneZ = 0;
                }

                if (!float.TryParse(currentSplitLine[4].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out cubeZ))
                {
                    cubeZ = 0;
                }
            }
            else if (currentSplitLine[0] == "rot_x")
            {
                if (!float.TryParse(currentSplitLine[1].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out headPlaneRotX))
                {
                    headPlaneRotX = 0;
                }

                if (!float.TryParse(currentSplitLine[2].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out abdomenPlaneRotX))
                {
                    abdomenPlaneRotX = 0;
                }

                if (!float.TryParse(currentSplitLine[3].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out femurPlaneRotX))
                {
                    femurPlaneRotX = 0;
                }

                if (!float.TryParse(currentSplitLine[4].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out cubeRotX))
                {
                    cubeRotX = 0;
                }
            }
            else if (currentSplitLine[0] == "rot_y")
            {
                if (!float.TryParse(currentSplitLine[1].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out headPlaneRotY))
                {
                    headPlaneRotY = 0;
                }

                if (!float.TryParse(currentSplitLine[2].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out abdomenPlaneRotY))
                {
                    abdomenPlaneRotY = 0;
                }

                if (!float.TryParse(currentSplitLine[3].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out femurPlaneRotY))
                {
                    femurPlaneRotY = 0;
                }

                if (!float.TryParse(currentSplitLine[4].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out cubeRotY))
                {
                    cubeRotY = 0;
                }
            }
            else if (currentSplitLine[0] == "rot_z")
            {
                if (!float.TryParse(currentSplitLine[1].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out headPlaneRotZ))
                {
                    headPlaneRotZ = 0;
                }

                if (!float.TryParse(currentSplitLine[2].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out abdomenPlaneRotZ))
                {
                    abdomenPlaneRotZ = 0;
                }

                if (!float.TryParse(currentSplitLine[3].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out femurPlaneRotZ))
                {
                    femurPlaneRotZ = 0;
                }

                if (!float.TryParse(currentSplitLine[4].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out cubeRotZ))
                {
                    cubeRotZ = 0;
                }
            }
        }

        standardPlaneLocations = new StandardPlaneLocations
        {
            HeadPlaneX = headPlaneX,
            AbdomenPlaneX = abdomenPlaneX,
            FemurPlaneX = femurPlaneX,
            HeadPlaneY = headPlaneY,
            AbdomenPlaneY = abdomenPlaneY,
            FemurPlaneY = femurPlaneY,
            HeadPlaneZ = headPlaneZ,
            AbdomenPlaneZ = abdomenPlaneZ,
            FemurPlaneZ = femurPlaneZ,
            HeadPlaneRotX = headPlaneRotX,
            AbdomenPlaneRotX = abdomenPlaneRotX,
            FemurPlaneRotX = femurPlaneRotX,
            HeadPlaneRotY = headPlaneRotY,
            AbdomenPlaneRotY = abdomenPlaneRotY,
            FemurPlaneRotY = femurPlaneRotY,
            HeadPlaneRotZ = headPlaneRotZ,
            AbdomenPlaneRotZ = abdomenPlaneRotZ,
            FemurPlaneRotZ = femurPlaneRotZ
        };
        //bool endOfFile = false;

        //while (!endOfFile)
        //{
        //    string currentLineOfFile = streamReader.ReadLine();

        //    if (currentLineOfFile == null)
        //    {
        //        endOfFile = true;
        //        break;
        //    }

        //    var valuesOfCurrentLine = currentLineOfFile.Split(',');
        //}
    }

    
    public void PlaceStandardPlanes()
    {
        //headStandardPlane.transform.localScale = new Vector3(rootObj.transform.lossyScale.x, rootObj.transform.lossyScale.y, rootObj.transform.lossyScale.z);
        headStandardPlane.transform.localPosition = new Vector3(headPlaneX, headPlaneY, headPlaneZ);
        headStandardPlane.transform.localRotation = Quaternion.Euler(new Vector3(headPlaneRotX, headPlaneRotY, headPlaneRotZ));

        abdomenStandardPlane.transform.localPosition = new Vector3(abdomenPlaneX, abdomenPlaneY, abdomenPlaneZ);
        abdomenStandardPlane.transform.localRotation = Quaternion.Euler(new Vector3(abdomenPlaneRotX, abdomenPlaneRotY, abdomenPlaneRotZ));

        femurStandardPlane.transform.localPosition = new Vector3(femurPlaneX, femurPlaneY, femurPlaneZ);
        femurStandardPlane.transform.localRotation = Quaternion.Euler(new Vector3(femurPlaneRotX, femurPlaneRotY, femurPlaneRotZ));
    }

    public void PlaceCube()
    {
        cube.transform.localPosition = new Vector3(cubeX, cubeY, cubeZ);
        cube.transform.localRotation = Quaternion.Euler(new Vector3(cubeRotX, cubeRotY, cubeRotZ));
    }

    void CreateProbeSidedEdgeLinesForAllStandardPlanes(GameObject standardPlane, int identifier)
    {
        // ----------- Creation of probe sided edge Line ------------------------------------------------------------------------------
        var probeSidedEdgeLineOfUsImage = new GameObject();
        probeSidedEdgeLineOfUsImage.name = "ProbeSidedEdgeLine_CloneUSPlane_" + identifier;
        probeSidedEdgeLineOfUsImage.tag = "ProbeSidedLine";
        probeSidedEdgeLineOfUsImage.AddComponent<MeshFilter>();

        probeSidedEdgeLineOfUsImage.AddComponent<MeshRenderer>();

        
        
        LineRenderer probeSidedEdgeOfUSPlane = probeSidedEdgeLineOfUsImage.AddComponent<LineRenderer>();

        probeSidedEdgeOfUSPlane.transform.parent = babyModel.transform;

        probeSidedEdgeOfUSPlane.gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        probeSidedEdgeOfUSPlane.SetWidth(0.01F, 0.01F);
        probeSidedEdgeOfUSPlane.SetVertexCount(2);

        CornerVerticesProbePlane = new List<Vector3>();
        LocalVerticesProbePlane = new List<Vector3>();
        GlobalVerticesProbePlane = new List<Vector3>();

        GetVertices(standardPlane);
        probeSidedEdgeOfUSPlane.SetPosition(0, CornerVerticesProbePlane[0]);
        probeSidedEdgeOfUSPlane.SetPosition(1, CornerVerticesProbePlane[1]);
    }

    void GetVertices(GameObject planeObject)
    {
       // CurrentGameObject = planeObject;

        // Probe plane
        LocalVerticesProbePlane = new List<Vector3>(planeObject.GetComponent<MeshFilter>().mesh.vertices);
        GlobalVerticesProbePlane.Clear();
        CornerVerticesProbePlane.Clear();

        foreach (Vector3 point in LocalVerticesProbePlane)
        {
            GlobalVerticesProbePlane.Add(planeObject.transform.TransformPoint(point));
        }

        foreach (int id in CornerIDs)
        {
            CornerVerticesProbePlane.Add(GlobalVerticesProbePlane[id]);
        }
    }
}
