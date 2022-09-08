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

    public float headPlaneRotW;
    public float abdomenPlaneRotW;
    public float femurPlaneRotW;

    public float cubeX;
    public float cubeY;
    public float cubeZ;
    public float cubeRotX;
    public float cubeRotY;
    public float cubeRotZ;
    public float cubeRotW;

    public float EulerRotationX;
    public float EulerRotationY;
    public float EulerRotationZ;

    public float probeX;
    public float probeY;
    public float probeZ;
    public float probeRotX;
    public float probeRotY;
    public float probeRotZ;
    public float probeRotW;

    private bool lineRenderingEnabled = false;
    private bool disableProbeSidedLanes = true;

    public float RotX;
    public List<Vector3> CornerVerticesProbePlane;
    List<Vector3> LocalVerticesProbePlane;
    List<Vector3> GlobalVerticesProbePlane;
    GameObject probeSidedEdgeLineOfUsImage;

    List<int> CornerIDs = new List<int> { 0, 10, 110, 120 };

    public GameObject headStandardPlane;
    GameObject abdomenStandardPlane;
    GameObject femurStandardPlane;
    public GameObject cube;
    public GameObject probe;

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
        probe = GameObject.Find("GE_Voluson_2D_aligned");

        ReadCsv();
        PlaceStandardPlanes();
        PlaceCube();
        PlaceCube();

        //CreateProbeSidedEdgeLinesForAllStandardPlanes(headStandardPlane, 1);
        //CreateProbeSidedEdgeLinesForAllStandardPlanes(abdomenStandardPlane, 2);
        //CreateProbeSidedEdgeLinesForAllStandardPlanes(femurStandardPlane, 3);

        //createdProbeSidedLines = GameObject.FindGameObjectsWithTag("ProbeSidedLine");
        //createdPlanes = GameObject.FindGameObjectsWithTag("StandardPlane");
        //lineRenderingEnabled = true;
    }

    void Update()
    {
        //if (lineRenderingEnabled && !interactableOfCheckBox.IsToggled)
        //{
        //    for (int i = 0; i < createdProbeSidedLines.Length; i++)
        //    {
        //        var currentLine = createdProbeSidedLines[i];
        //        var currentPlane = createdPlanes[i];

        //        var lineRenderer = currentLine.GetComponent<LineRenderer>();

        //        GetVertices(currentPlane);

        //        lineRenderer.SetPosition(0, CornerVerticesProbePlane[0]);
        //        lineRenderer.SetPosition(1, CornerVerticesProbePlane[1]);
        //    }  
        //}
    }

    public void DeactivateLineRendering()
    {
        lineRenderingEnabled = false;
    }

    public void ReactivateLineRendering()
    {
        lineRenderingEnabled = true;
    }

    private void ReadCsv()
    {
        using (StreamReader strReader = new StreamReader("Assets/GameObjectLocations.csv"))
        {
           
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

            probeX = 0f;
            probeY = 0f;
            probeZ = 0f;
            probeRotX = 0f;
            probeRotY = 0f;
            probeRotZ = 0f;

            bool endOfFile = false;

            string currentLine;

            while (!endOfFile)
            {
                currentLine = strReader.ReadLine();
                
                if (currentLine == null)
                {
                    endOfFile = true;
                    break;
                }

                var currentSplitLine = currentLine.Split(',');

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

                    if (!float.TryParse(currentSplitLine[4].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out probeX))
                    {
                        probeX = 0;
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

                    if (!float.TryParse(currentSplitLine[4].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out probeY))
                    {
                        probeY = 0;
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

                    if (!float.TryParse(currentSplitLine[4].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out probeZ))
                    {
                        probeZ = 0;
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

                    if (!float.TryParse(currentSplitLine[4].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out probeRotX))
                    {
                        probeRotX = 0;
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

                    if (!float.TryParse(currentSplitLine[4].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out probeRotY))
                    {
                        probeRotY = 0;
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

                    if (!float.TryParse(currentSplitLine[4].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out probeRotZ))
                    {
                        probeRotZ = 0;
                    }
                }
                else if (currentSplitLine[0] == "rot_w")
                {
                    if (!float.TryParse(currentSplitLine[1].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out headPlaneRotW))
                    {
                        headPlaneRotW = 0;
                    }

                    if (!float.TryParse(currentSplitLine[2].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out abdomenPlaneRotW))
                    {
                        abdomenPlaneRotW = 0;
                    }

                    if (!float.TryParse(currentSplitLine[3].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out femurPlaneRotW))
                    {
                        femurPlaneRotW = 0;
                    }

                    if (!float.TryParse(currentSplitLine[4].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out cubeRotW))
                    {
                        cubeRotW = 0;
                    }

                    if (!float.TryParse(currentSplitLine[4].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out probeRotW))
                    {
                        probeRotW = 0;
                    }
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
    }

    public void PlaceStandardPlanes()
    {
        headStandardPlane.transform.localPosition = new Vector3(headPlaneX, headPlaneY, headPlaneZ);
        headStandardPlane.transform.localRotation = new Quaternion(headPlaneRotX, headPlaneRotY, headPlaneRotZ, headPlaneRotW);

        EulerRotationX = headStandardPlane.transform.localRotation.x;
        EulerRotationY = headStandardPlane.transform.localRotation.y;
        EulerRotationZ = headStandardPlane.transform.localRotation.z;

        abdomenStandardPlane.transform.localPosition = new Vector3(abdomenPlaneX, abdomenPlaneY, abdomenPlaneZ);
        abdomenStandardPlane.transform.localRotation = new Quaternion(abdomenPlaneRotX, abdomenPlaneRotY, abdomenPlaneRotZ, abdomenPlaneRotW);

        femurStandardPlane.transform.localPosition = new Vector3(femurPlaneX, femurPlaneY, femurPlaneZ);
        femurStandardPlane.transform.localRotation = new Quaternion(femurPlaneRotX, femurPlaneRotY, femurPlaneRotZ, femurPlaneRotW);
    }

    public void PlaceCube()
    {
        cube.transform.localPosition = new Vector3(cubeX, cubeY, cubeZ);
        cube.transform.localRotation = new Quaternion(cubeRotX, cubeRotY, cubeRotZ, cubeRotW);
    }

    public void PlaceProbe()
    {
        probe.transform.localPosition = new Vector3(probeX, probeY, probeZ);
        probe.transform.localRotation = new Quaternion(probeRotX, probeRotY, probeRotZ, probeRotW);
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
