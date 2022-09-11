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
    //public VideoPlayer VideoPlayback;
    //public RawImage thumbnailVid;
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

        //probeSidedEdgeLineOfUsImage = new GameObject();
        //probeSidedEdgeLineOfUsImage.name = "ProbeSidedEdgeLine_CloneUSPlane" + imageCounter;;
        //probeSidedEdgeLineOfUsImage.AddComponent<MeshFilter>();


        //probeSidedEdgeOfUSPlane = probeSidedEdgeLineOfUsImage.AddComponent<LineRenderer>();
        //probeSidedEdgeOfUSPlane.gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        //probeSidedEdgeOfUSPlane.SetWidth(0.01F, 0.01F);
        //probeSidedEdgeOfUSPlane.SetVertexCount(2);

        //CornerVerticesProbePlane = new List<Vector3>();
        //LocalVerticesProbePlane = new List<Vector3>();
        //GlobalVerticesProbePlane = new List<Vector3>();
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

        //duplicate.name = "USPlane" + imageCounter;
        nameOfCurrentUltrasoundPlane = duplicate.name;

        AttachLabel(duplicate);

        // Make the newly created plane object a child of the baby model.
        duplicate.transform.parent = babyModel.transform;

        // ----------- Creation of probe sided edge Line ------------------------------------------------------------------------------
        probeSidedEdgeLineOfUsImage = new GameObject();
        probeSidedEdgeLineOfUsImage.name = "ProbeSidedEdgeLine_CloneUSPlane" + imageCounter; 
        probeSidedEdgeLineOfUsImage.tag = "ProbeSidedLine";
        probeSidedEdgeLineOfUsImage.AddComponent<MeshFilter>();

        probeSidedEdgeLineOfUsImage.AddComponent<MeshRenderer>();
        //probeSidedEdgeLineOfUsImage.AddComponent<BoxCollider>();

        probeSidedEdgeOfUSPlane = probeSidedEdgeLineOfUsImage.AddComponent<LineRenderer>();
       
        probeSidedEdgeOfUSPlane.gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        probeSidedEdgeOfUSPlane.SetWidth(0.01F, 0.01F);
        probeSidedEdgeOfUSPlane.SetVertexCount(2);

        //probeSidedEdgeOfUSPlane = duplicate.AddComponent<LineRenderer>();
        //probeSidedEdgeOfUSPlane.gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        //probeSidedEdgeOfUSPlane.SetWidth(0.01F, 0.01F);
        //probeSidedEdgeOfUSPlane.SetVertexCount(2);

        CornerVerticesProbePlane = new List<Vector3>();
        LocalVerticesProbePlane = new List<Vector3>();
        GlobalVerticesProbePlane = new List<Vector3>();

        GetVertices(rootObj);
        probeSidedEdgeOfUSPlane.SetPosition(0, CornerVerticesProbePlane[0]);
        probeSidedEdgeOfUSPlane.SetPosition(1, CornerVerticesProbePlane[1]);

        //AddColliderToLine(probeSidedEdgeOfUSPlane, CornerVerticesProbePlane[0], CornerVerticesProbePlane[1]);

        lineRenderingEnabled = true;

        //probeSidedEdgeLineOfUsImage.AddComponent<MeshCollider>();

        // ---------------------------------------------------------------------------------------------------

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

        //videoPlayer.frameReady -= FrameReady;
    }

    void Prepared(VideoPlayer vp) => vp.Pause();

    //void FrameReady(VideoPlayer vp, long frameIndex)
    //{
    //   Console.WriteLine("FrameReady " + frameIndex);
    //    var textureToCopy = vp.texture;

    //    rend.material.mainTexture = textureToCopy;
    //    rend.material.SetTexture("Default-Diffuse", Get2DTexture(vp));

    //    // Perform texture copy here ...
    //    vp.frame = frameIndex + 1;
    //}

    private void AttachLabel(GameObject duplicate)
    {
        GameObject originalTextLabel;

        if (duplicate.name == "HeadPlane")
        {
            originalTextLabel = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(g => g.CompareTag("USPlaneLabel"));
            //originalTextLabel = GameObject.Find("HeadLabelForUsImage");
            //originalTextLabel = GameObject.Find("AbdomenLabelForUsImage");
            originalTextLabel.SetActive(true);
        }
        else if (duplicate.name == "AbdomenPlane")
        {
            //originalTextLabel = GameObject.Find("AbdomenLabelForUsImage");
            originalTextLabel = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(g => g.CompareTag("USPlaneLabel2"));
            originalTextLabel.SetActive(true);
        }
        else if (duplicate.name == "FemurPlane")
        {
            //originalTextLabel = GameObject.Find("FemurLabelForUsImage");
            originalTextLabel = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(g => g.CompareTag("USPlaneLabel3"));
            originalTextLabel.SetActive(true);
        }
        else
        {
            // Nothing to do, so return.
            return;
        }

        //Quaternion rotation = originalTextLabel.transform.rotation;
        //Vector3 position = originalTextLabel.transform.position;

        //var ultrasoundImage = GameObject.Find(nameOfTargetPlane);
        //foundPlane = ultrasoundImage;

        originalTextLabel.transform.parent = duplicate.transform;

        //originalTextLabel.transform.localPosition = new Vector3(0.1791698f, 0.1134472f, 1.4507f);
        originalTextLabel.transform.localPosition = new Vector3(5f, 0f, 5.5f);//new Vector3(-5.5f, 0f, 8f);
        originalTextLabel.transform.localRotation = Quaternion.Euler(90f, 90f, 90f);//duplicate.transform.localRotation; //Quaternion.Euler(90f, -4f, 176f); //new Quaternion(90f, -4f, 0.03489959f, 0.999391f);
        originalTextLabel.transform.localScale = new Vector3(1f, 1f, 1f);

        duplicateLocalRotation = originalTextLabel.transform.localRotation;

    }

    // Update is called once per frame
    void Update()
    {
        if (lineRenderingEnabled)
        {
            LinePosOne = CornerVerticesProbePlane[0];
            LinePosTwo = CornerVerticesProbePlane[1];

            GetVertices(duplicate);
            probeSidedEdgeOfUSPlane.SetPosition(0, CornerVerticesProbePlane[0]);
            probeSidedEdgeOfUSPlane.SetPosition(1, CornerVerticesProbePlane[1]);
        }
    }

    //public void VideoPreparation(string path_)
    //{
    //    videoPlayer.url = path_;
    //    videoPlayer.Stop();
    //    videoPlayer.renderMode = VideoRenderMode.APIOnly;
    //    videoPlayer.sendFrameReadyEvents = true;
    //    videoPlayer.frameReady += FrameReady2;
    //    videoPlayer.Prepare();
    //    StartCoroutine(PrepareVideo());
    //}

    void FrameReady(VideoPlayer vp, long frameIndex)
    {
        //Debug.Log("FrameReady " + frameIndex);
        //videoPlayer.Pause();
        //thumbnailVid.texture = vp.texture;
        //rend.material.mainTexture = vp.texture;

        //vp.frame = frameIndex + 3000;
        //thumbnailVid.texture = Get2DTexture();
        rend.material.mainTexture = Get2DTexture(vp);
        rendererOfMirroredPlane.material.mainTexture = rend.material.mainTexture;

        //vp.frame = frameIndex + 30;

        //rend.material.SetTexture("Default-Diffuse", Get2DTexture(vp));

        //rend.material.mainTexture = Get2DTexture(vp);
        //videoPlayer.sendFrameReadyEvents = false; //To stop frameReady events

        vp = null;
        
        thumbnailOk = true;

        videoPlayer.frameReady -= FrameReady;
        //vp.frameReady -= FrameReady;
    }

    IEnumerator PrepareVideo()
    {
        yield return new WaitUntil(() => videoPlayer.isPrepared);

        //Debug.Log("Video PlayBack");
        videoPlayer.Play();

        //vidWidth = Convert.ToInt32(videoPlayer.width);
        //vidHeight = Convert.ToInt32(videoPlayer.height);

        videoPlayer.isLooping = true;
        videoPlayer.renderMode = VideoRenderMode.MaterialOverride;

        //Debug.Log("Video height & width: " + vidWidth + ", " + vidHeight);

        yield return new WaitUntil(() => thumbnailOk);

        videoPlayer.Play();

        GC.Collect();

        //videoPlayer.frameReady -= FrameReady;
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

    
        //UnityEngine.Color[] pixels = thumbnail.GetPixels();

        RenderTexture.active = cTexture;

        rTexture.Release();

        ////-------------------------------
        //Texture2D destTex = new Texture2D(vp.texture.width, vp.texture.height);
        //destTex.SetPixels(pixels);
        //destTex.Apply();
        ////-------------------------------

        return thumbnail;
        //return destTex;
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
        //create the collider for the line
        BoxCollider lineCollider = new GameObject("LineCollider").AddComponent<BoxCollider>();
        //set the collider as a child of your line
        lineCollider.transform.parent = line.transform;
        // get width of collider from line 
        float lineWidth = line.endWidth;
        // get the length of the line using the Distance method
        float lineLength = Vector3.Distance(startPoint, endPoint);
        // size of collider is set where X is length of line, Y is width of line
        //z will be how far the collider reaches to the sky
        lineCollider.size = new Vector3(lineLength, lineWidth, 0.01f);
        // get the midPoint
        Vector3 midPoint = (startPoint + endPoint) / 2;
        // move the created collider to the midPoint
        lineCollider.transform.position = midPoint;


        //heres the beef of the function, Mathf.Atan2 wants the slope, be careful however because it wants it in a weird form
        //it will divide for you so just plug in your (y2-y1),(x2,x1)
        float angle = Mathf.Atan2((endPoint.z - startPoint.z), (endPoint.x - startPoint.x));

        // angle now holds our answer but it's in radians, we want degrees
        // Mathf.Rad2Deg is just a constant equal to 57.2958 that we multiply by to change radians to degrees
        angle *= Mathf.Rad2Deg;

        //were interested in the inverse so multiply by -1
        angle *= -1;
        // now apply the rotation to the collider's transform, carful where you put the angle variable
        // in 3d space you don't wan't to rotate on your y axis
        lineCollider.transform.Rotate(0, angle, 0);
    }
}
