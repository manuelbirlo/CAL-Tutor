using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using UnityEditor;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.XR.WSA;
using TMPro;

public class SaveAndLoadPlanes : MonoBehaviour
{
    public Quaternion localRot;
    public Transform TextLabel;

    Rect _Save, _Load, _SaveMSG, _LoadMSG;
    bool _ShouldSave, _ShouldLoad, _SwitchSave, _SwitchLoad;
    public string _FileLocation, _FileName;
   
    UserData myData;
  
    string _data;

    public float X;
    public float Y;
    public float Z;

    public GameObject[] CreatedPlanes;
    public GameObject[] CreatedProbeSidedLines;

    Vector3 VPosition;

    private int imageCounter;

    public List<Vector3> CornerVerticesProbePlane;
    public List<Vector3> LocalVerticesProbePlane;
    public List<Vector3> GlobalVerticesProbePlane;

    public Vector3 posOne;
    public Vector3 posTwo;

    List<int> CornerIDs = new List<int> { 0, 10, 110, 120 };

    void Start()
    {
        imageCounter = 1;

        CornerVerticesProbePlane = new List<Vector3>();
        LocalVerticesProbePlane = new List<Vector3>();
        GlobalVerticesProbePlane = new List<Vector3>();

        _Save = new Rect(10, 80, 100, 20);
        _Load = new Rect(10, 100, 100, 20);
        _SaveMSG = new Rect(10, 120, 400, 40);
        _LoadMSG = new Rect(10, 140, 400, 40);

        _FileLocation = Application.persistentDataPath;
        _FileName = "SaveData.xml";

        myData = new UserData();
    }

    void Update() { }

    private List<Plane> GetNonSceneObjects()
    {
        List<Plane> objectsInScene = new List<Plane>();

        var result = Resources.FindObjectsOfTypeAll(typeof(Plane));
        Debug.Log(" TYPE: " + result.GetType());
        
        return objectsInScene;
    }

    public void Load() //OnGUI()
    {
        string filePath = _FileLocation + "\\" + _FileName;

        // If the .xml file that contains the relevant data doesn't exist, exit method here. 
        if (!File.Exists(filePath))
        {
            return;
        }

        // Load UserData into myData 
        LoadXML();

        if (_data.ToString() != "")
            {
                myData = (UserData)DeserializeObject(_data);
                
                for (int i = 0; i < myData._iUser.Length; i++)
                {
                    var currentPlane = myData._iUser[i];
                    
                    GameObject duplicate = GameObject.CreatePrimitive(PrimitiveType.Plane);

                    Renderer renderer = duplicate.GetComponent<Renderer>();

                    Create2DTextureFromPng(currentPlane.name, renderer);
                    
                    VPosition = new Vector3(currentPlane.x, currentPlane.y, currentPlane.z);

                    duplicate.transform.localScale = new Vector3(currentPlane.scaleX, currentPlane.scaleY, currentPlane.scaleZ);
                    duplicate.transform.position = VPosition;
                    duplicate.name = currentPlane.name;
                    duplicate.tag = "USPlane";
                    duplicate.transform.rotation = new Quaternion(currentPlane.rotationX, currentPlane.rotationY, currentPlane.rotationZ, currentPlane.rotationW);

                    var textLabelObject = new GameObject();
                    TextMeshPro textComponent = textLabelObject.AddComponent<TextMeshPro>();
                    textComponent.text = currentPlane.labelText;

                    // Create the textLabel child object.
                    textLabelObject.transform.parent = duplicate.transform;

                    textLabelObject.transform.localPosition = new Vector3(currentPlane.labelX, currentPlane.labelY, currentPlane.labelZ);
                    textLabelObject.transform.localRotation = new Quaternion(currentPlane.labelRotationX, currentPlane.labelRotationY, currentPlane.labelRotationZ, currentPlane.labelRotationW);
                
                    textLabelObject.transform.localScale = new Vector3(1f, 1f, 1f);
                    textComponent.fontSize = currentPlane.labelFontSize;

                    // ----------- Creation of probe sided edge Line ------------------------------------------------------------------------------
                    var probeSidedEdgeLineOfUsImage = new GameObject();
                    probeSidedEdgeLineOfUsImage.name = "Reloaded_ProbeSidedEdgeLine_CloneUSPlane" + currentPlane.imageNumber; //imageCounter;
                    probeSidedEdgeLineOfUsImage.tag = "ProbeSidedLine";
                    probeSidedEdgeLineOfUsImage.AddComponent<MeshFilter>();
                    probeSidedEdgeLineOfUsImage.AddComponent<MeshCollider>();

                    var probeSidedEdgeOfUSPlane = probeSidedEdgeLineOfUsImage.AddComponent<LineRenderer>();
                    probeSidedEdgeOfUSPlane.gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
                    probeSidedEdgeOfUSPlane.SetWidth(0.01F, 0.01F);
                    probeSidedEdgeOfUSPlane.SetVertexCount(2);

                    var firstCornerOfProbeSidedLine = new Vector3(currentPlane.probeSidedLineFirstCornerVerticesX, 
                                                                  currentPlane.probeSidedLineFirstCornerVerticesY, 
                                                                  currentPlane.probeSidedLineFirstCornerVerticesZ);

                    var secondCornerOfProbeSidedLine = new Vector3(currentPlane.probeSidedLineSecondCornerVerticesX, 
                                                                   currentPlane.probeSidedLineSecondCornerVerticesY, 
                                                                   currentPlane.probeSidedLineSecondCornerVerticesZ);

                    probeSidedEdgeOfUSPlane.SetPosition(0, firstCornerOfProbeSidedLine);
                    probeSidedEdgeOfUSPlane.SetPosition(1, secondCornerOfProbeSidedLine);

                    imageCounter++;
                }
            }
    }

    public void Save()
    {
        CreatedPlanes = GameObject.FindGameObjectsWithTag("USPlane"); 
        CreatedProbeSidedLines = GameObject.FindGameObjectsWithTag("ProbeSidedLine");

        myData = new UserData();
        myData._iUser = new UserData.DemoData[CreatedPlanes.Length];

        for (int i = 0; i < CreatedPlanes.Length; i++)
        {
            var currentUltrasoundPlane = CreatedPlanes[i];
            //var currentProbeSidedLane = CreatedProbeSidedLines[i];

            GetVertices(currentUltrasoundPlane);
        
            myData._iUser[i] = new UserData.DemoData();

            myData._iUser[i].x = currentUltrasoundPlane.transform.position.x;
            myData._iUser[i].y = currentUltrasoundPlane.transform.position.y;
            myData._iUser[i].z = currentUltrasoundPlane.transform.position.z;
            myData._iUser[i].name = currentUltrasoundPlane.name;

            myData._iUser[i].rotationX = currentUltrasoundPlane.transform.rotation.x;
            myData._iUser[i].rotationY = currentUltrasoundPlane.transform.rotation.y;
            myData._iUser[i].rotationZ = currentUltrasoundPlane.transform.rotation.z;
            myData._iUser[i].rotationW = currentUltrasoundPlane.transform.rotation.w;

            myData._iUser[i].scaleX = currentUltrasoundPlane.transform.lossyScale.x;
            myData._iUser[i].scaleY = currentUltrasoundPlane.transform.lossyScale.y;
            myData._iUser[i].scaleZ = currentUltrasoundPlane.transform.lossyScale.z;

            myData._iUser[i].probeSidedLineFirstCornerVerticesX = CornerVerticesProbePlane[0].x;
            myData._iUser[i].probeSidedLineFirstCornerVerticesY = CornerVerticesProbePlane[0].y;
            myData._iUser[i].probeSidedLineFirstCornerVerticesZ = CornerVerticesProbePlane[0].z;
            
            myData._iUser[i].probeSidedLineSecondCornerVerticesX = CornerVerticesProbePlane[1].x;
            myData._iUser[i].probeSidedLineSecondCornerVerticesY = CornerVerticesProbePlane[1].y;
            myData._iUser[i].probeSidedLineSecondCornerVerticesZ = CornerVerticesProbePlane[1].z;

            char planeNrAsChar = currentUltrasoundPlane.name[currentUltrasoundPlane.name.Length - 1];
            int planeNr = planeNrAsChar - '0';
            myData._iUser[i].imageNumber = planeNr;
            
            var textLabel = currentUltrasoundPlane.transform.GetChild(0);
            myData._iUser[i].labelX = textLabel.transform.localPosition.x;
            myData._iUser[i].labelY = textLabel.transform.localPosition.y;
            myData._iUser[i].labelZ = textLabel.transform.localPosition.z;

            myData._iUser[i].labelRotationW = textLabel.localRotation.w;
            myData._iUser[i].labelRotationX = textLabel.localRotation.x;
            myData._iUser[i].labelRotationY = textLabel.localRotation.y;
            myData._iUser[i].labelRotationZ = textLabel.localRotation.z;

            var textMeshProComponent = textLabel.GetComponent<TextMeshPro>();

            myData._iUser[i].labelFontSize = textMeshProComponent.fontSize;
            myData._iUser[i].labelText = textMeshProComponent.text;

            localRot = textLabel.localRotation;
            TextLabel = textLabel;            
        }

        // Create the xml
        _data = SerializeObject(myData);
        
        CreateXML();
    }

    public void Delete()
    {
        string filePath = _FileLocation + "\\" + _FileName;

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        string[] filesOfTypePng = System.IO.Directory.GetFiles(_FileLocation, "*.png");

        foreach (var imageName in filesOfTypePng)
        {
            if (imageName.Contains("USPlane"))
            {
                File.Delete(imageName);
            }
        }

    }

    private void Create2DTextureFromPng(string fileName, Renderer renderer)
    {
        var filePath = _FileLocation + "/" + fileName +".png";

        if (System.IO.File.Exists(filePath))
        {
            var bytes = System.IO.File.ReadAllBytes(filePath);
            var texture = new Texture2D(1,1);
            texture.LoadImage(bytes);

            renderer.material.mainTexture = texture;
        }
    }

    /* The following metods came from the referenced URL */
    string UTF8ByteArrayToString(byte[] characters)
    {
        UTF8Encoding encoding = new UTF8Encoding();
        string constructedString = encoding.GetString(characters);
        return (constructedString);
    }

    byte[] StringToUTF8ByteArray(string pXmlString)
    {
        UTF8Encoding encoding = new UTF8Encoding();
        byte[] byteArray = encoding.GetBytes(pXmlString);
        return byteArray;
    }

    // Here we serialize our UserData object of myData 
    string SerializeObject(object pObject)
    {
        string XmlizedString = null;
        MemoryStream memoryStream = new MemoryStream();
        XmlSerializer xs = new XmlSerializer(typeof(UserData));
        XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        xs.Serialize(xmlTextWriter, pObject);
        memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
        XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
        return XmlizedString;
    }

    object DeserializeObject(string pXmlizedString)
    {
        XmlSerializer xs = new XmlSerializer(typeof(UserData));
        MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
        XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        return xs.Deserialize(memoryStream);
    }

    void CreateXML()
    {
        StreamWriter writer;
        FileInfo fileInfo = new FileInfo(_FileLocation + "\\" + _FileName);

        if (!fileInfo.Exists)
        {
            writer = fileInfo.CreateText();
        }
        else
        {
            fileInfo.Delete();
            writer = fileInfo.CreateText();
        }

        writer.Write(_data);
        writer.Close();
    }

    void LoadXML()
    {
        StreamReader r = File.OpenText(_FileLocation + "\\" + _FileName);
        string _info = r.ReadToEnd();
        r.Close();
        _data = _info;
    }

    void GetVertices(GameObject plane)
    {
        LocalVerticesProbePlane = new List<Vector3>(plane.GetComponent<MeshFilter>().mesh.vertices);
        GlobalVerticesProbePlane.Clear();
        CornerVerticesProbePlane.Clear();

        foreach (Vector3 point in LocalVerticesProbePlane)
        {
            GlobalVerticesProbePlane.Add(plane.transform.TransformPoint(point));
        }

        foreach (int id in CornerIDs)
        {
            CornerVerticesProbePlane.Add(GlobalVerticesProbePlane[id]);
        }
    }
}

[System.Serializable]
public class UserData
{
    public DemoData[] _iUser;
    public UserData() { }

    public class DemoData
    {
        public float x;
        public float y;
        public float z;

        public float scaleX;
        public float scaleY;
        public float scaleZ;

        public float rotationX;
        public float rotationY;
        public float rotationZ;
        public float rotationW;

        public float probeSidedLineFirstCornerVerticesX;
        public float probeSidedLineFirstCornerVerticesY;
        public float probeSidedLineFirstCornerVerticesZ;

        public float probeSidedLineSecondCornerVerticesX;
        public float probeSidedLineSecondCornerVerticesY;
        public float probeSidedLineSecondCornerVerticesZ;

        public string labelText;

        public float labelX;
        public float labelY;
        public float labelZ;

        public float labelRotationW;
        public float labelRotationX;
        public float labelRotationY;
        public float labelRotationZ;
        public float labelFontSize;

        public string imageName;

        public int imageNumber;

        public string name;
    }
}
