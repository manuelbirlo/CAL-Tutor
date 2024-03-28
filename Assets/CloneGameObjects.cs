using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class CloneGameObjects : MonoBehaviour
{
    public GameObject rootObj;
    public GameObject babyModel;

    public Quaternion duplicateLocalRotation;

    public Vector3 LinePosOne;
    public Vector3 LinePosTwo;
     
    // TODO: remove this one, just for debugging.
    public GameObject CurrentGameObject;


    public int vidHeight;
    public int vidWidth;
   
    private Texture2D thumbnail;
    bool thumbnailOk;
    private VideoPlayer videoPlayer;

    private GameObject duplicate;
    private Renderer rend;
    private Renderer rendererOfMirroredPlane;
    public string _FileLocation, _FileName;

    public int imageCounter;

    private string nameOfCurrentUltrasoundPlane;

    public bool lineRenderingEnabled = false;

    LineRenderer probeSidedEdgeOfUSPlane;
    public List<Vector3> CornerVerticesProbePlane;
    List<Vector3> LocalVerticesProbePlane;
    List<Vector3> GlobalVerticesProbePlane;
    GameObject probeSidedEdgeLineOfUsImage;

    List<int> CornerIDs = new List<int> { 0, 10, 110, 120 };

    public Transform mirroredPlaneObject;

    // Start is called before the first frame update
    void Start()
    {
        imageCounter = 1;
        _FileLocation = Application.persistentDataPath;
        _FileName = "UltrasoundPlane.png";
    }

    public void Clone()
    {
        duplicate = GameObject.CreatePrimitive(PrimitiveType.Plane);
        duplicate.tag = "USPlane";
        duplicate.transform.localScale = new Vector3(rootObj.transform.lossyScale.x, rootObj.transform.lossyScale.y, rootObj.transform.lossyScale.z);
        duplicate.transform.position = rootObj.transform.position;
        duplicate.transform.rotation = rootObj.transform.rotation;

        if (imageCounter == 1)
        {
            duplicate.name = "HeadPlane";
        }
        else if (imageCounter == 2)
        {
            duplicate.name = "AbdomenPlane";
        }
        else if (imageCounter == 3)
        {
            duplicate.name = "FemurPlane";
        }

        nameOfCurrentUltrasoundPlane = duplicate.name;

        AttachLabel(duplicate);

        // Make the newly created plane object a child of the baby model.
        duplicate.transform.parent = babyModel.transform;

        imageCounter++;
        videoPlayer = rootObj.GetComponent<VideoPlayer>();

        rend = duplicate.GetComponent<Renderer>();

        //videoPlayer.Stop();
        videoPlayer.renderMode = VideoRenderMode.APIOnly;
        videoPlayer.prepareCompleted += Prepared;
        videoPlayer.sendFrameReadyEvents = true;
        videoPlayer.frameReady += FrameReady;
        videoPlayer.Prepare();

        StartCoroutine(PrepareVideo());

        GameObject mirroredPlane = GameObject.Instantiate(duplicate);
        mirroredPlane.tag = "Untagged";
        mirroredPlane.name = duplicate.name + "Mirrored";
        mirroredPlane.transform.parent = duplicate.transform;

        mirroredPlane.transform.localPosition = new Vector3(0, 0, 0);
        mirroredPlane.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
        mirroredPlane.transform.localScale = new Vector3(1, 1, 1);

        GameObject textLabel;

        if (mirroredPlane.name == "HeadPlaneMirrored")
        {
            mirroredPlaneObject = mirroredPlane.transform.FindChild("HeadLabelForUsImage");
            textLabel = mirroredPlane.transform.FindChild("HeadLabelForUsImage").gameObject;
            textLabel.SetActive(false);
        }
        else if (mirroredPlane.name == "AbdomenPlaneMirrored")
        {
            textLabel = mirroredPlane.transform.FindChild("AbdomenLabelForUsImage").gameObject;
            textLabel.SetActive(false);
        }
        else if (mirroredPlane.name == "FemurPlaneMirrored")
        {
            
            textLabel = mirroredPlane.transform.FindChild("FemurLabelForUsImage").gameObject;
           
            textLabel.SetActive(false);
        }

        rendererOfMirroredPlane = mirroredPlane.GetComponent<Renderer>();
    }

    void Prepared(VideoPlayer vp) => vp.Pause();

    private void AttachLabel(GameObject duplicate)
    {
        GameObject originalTextLabel;

        if (duplicate.name == "HeadPlane")
        {
            originalTextLabel = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(g => g.CompareTag("USPlaneLabel"));
            originalTextLabel.SetActive(true);
        }
        else if (duplicate.name == "AbdomenPlane")
        {
            originalTextLabel = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(g => g.CompareTag("USPlaneLabel2"));
            originalTextLabel.SetActive(true);
        }
        else if (duplicate.name == "FemurPlane")
        {
            originalTextLabel = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(g => g.CompareTag("USPlaneLabel3"));
            originalTextLabel.SetActive(true);
        }
        else
        {
            // Nothing to do, so return.
            return;
        }
        
        originalTextLabel.transform.parent = duplicate.transform;

        originalTextLabel.transform.localPosition = new Vector3(5f, 0f, 5.5f);
        originalTextLabel.transform.localRotation = Quaternion.Euler(90f, 90f, 90f);
        originalTextLabel.transform.localScale = new Vector3(1f, 1f, 1f);

        duplicateLocalRotation = originalTextLabel.transform.localRotation;

    }

    // Update is called once per frame
    void Update()
    {
    }

    void FrameReady(VideoPlayer vp, long frameIndex)
    {
        rend.material.mainTexture = Get2DTexture(vp);
        rendererOfMirroredPlane.material.mainTexture = rend.material.mainTexture;
        nts = false; //To stop frameReady events

        vp = null;
        
        thumbnailOk = true;

        videoPlayer.frameReady -= FrameReady;
    }

    IEnumerator PrepareVideo()
    {
        yield return new WaitUntil(() => videoPlayer.isPrepared);

        videoPlayer.Play();
        videoPlayer.isLooping = true;
        videoPlayer.renderMode = VideoRenderMode.MaterialOverride;

        yield return new WaitUntil(() => thumbnailOk);

        videoPlayer.Play();

        GC.Collect();
    }

    private Texture2D Get2DTexture(VideoPlayer vp)
    {
        thumbnail = new Texture2D(vp.texture.width, vp.texture.height, TextureFormat.RGBA32, false);
        RenderTexture cTexture = RenderTexture.active;
        RenderTexture rTexture = new RenderTexture(vp.texture.width, vp.texture.height, 32);
     
        UnityEngine.Graphics.Blit(vp.texture, rTexture);

        RenderTexture.active = rTexture;
        thumbnail.ReadPixels(new Rect(0, 0, rTexture.width, rTexture.height), 0, 0);
        thumbnail.Apply();
        
        // Save the image to the file system so that it can be reloaded later on (by another script.S)
        byte[] itemBGBytes = thumbnail.EncodeToPNG();
        File.WriteAllBytes(_FileLocation + "/" + nameOfCurrentUltrasoundPlane + ".png", itemBGBytes);

        RenderTexture.active = cTexture;

        rTexture.Release();

        return thumbnail;
    }

    void GetVertices(GameObject planeObject)
    {
        CurrentGameObject = planeObject;

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

    private void AddColliderToLine(LineRenderer line, Vector3 startPoint, Vector3 endPoint)
    {
        // Create the collider for the line
        BoxCollider lineCollider = new GameObject("LineCollider").AddComponent<BoxCollider>();
        // Set the collider as a child of the line
        lineCollider.transform.parent = line.transform;
        // Get width of collider from line 
        float lineWidth = line.endWidth;
        // Get the length of the line using the Distance method
        float lineLength = Vector3.Distance(startPoint, endPoint);
        // Size of collider is set where X = length of line, Y = width of line
        lineCollider.size = new Vector3(lineLength, lineWidth, 0.01f);
        // Get the midPoint
        Vector3 midPoint = (startPoint + endPoint) / 2;
        // Move the created collider to the midPoint
        lineCollider.transform.position = midPoint;

        float angle = Mathf.Atan2((endPoint.z - startPoint.z), (endPoint.x - startPoint.x));
        // Convert from RAD to DEG
        angle *= Mathf.Rad2Deg;

        //Get the inverse: multiply by -1
        angle *= -1;
        // Apply the rotation to the collider's transform
        // Rotate GameObject in 3D space along its y-axis
        lineCollider.transform.Rotate(0, angle, 0);
    }
}
