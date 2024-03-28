using UnityEngine;
using System.Collections;
using System.Linq;


public class VideoCaptureScript : MonoBehaviour
{
    private bool stopRecording = false;

    UnityEngine.Windows.WebCam.VideoCapture m_VideoCapture = null;
    
    void Start()
    {
    }

    void Update()
    {
    }

    public void StartVideoCapture()
    {
        UnityEngine.Windows.WebCam.VideoCapture.CreateAsync(false, OnVideoCaptureCreated);
    }

    void OnVideoCaptureCreated(UnityEngine.Windows.WebCam.VideoCapture videoCapture)
    {
        if (videoCapture != null)
        {
            m_VideoCapture = videoCapture;

            Resolution cameraResolution = UnityEngine.Windows.WebCam.VideoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
            float cameraFramerate = UnityEngine.Windows.WebCam.VideoCapture.GetSupportedFrameRatesForResolution(cameraResolution).OrderByDescending((fps) => fps).First();

            UnityEngine.Windows.WebCam.CameraParameters cameraParameters = new UnityEngine.Windows.WebCam.CameraParameters();
            cameraParameters.hologramOpacity = 0.0f;
            cameraParameters.frameRate = cameraFramerate;
            cameraParameters.cameraResolutionWidth = cameraResolution.width;
            cameraParameters.cameraResolutionHeight = cameraResolution.height;
            cameraParameters.pixelFormat = UnityEngine.Windows.WebCam.CapturePixelFormat.BGRA32;

            m_VideoCapture.StartVideoModeAsync(cameraParameters,
                                                UnityEngine.Windows.WebCam.VideoCapture.AudioState.None,
                                                OnStartedVideoCaptureMode);
        }
        else
        {
            Debug.LogError("Failed to create VideoCapture Instance!");
        }
    }

    //public void StartVideoCapture()
    //{
    //    Debug.Log("Entered StartVideoCapture method.");

    //    Resolution cameraResolution = UnityEngine.Windows.WebCam.VideoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
    //    Debug.Log(cameraResolution);

    //    float cameraFramerate = UnityEngine.Windows.WebCam.VideoCapture.GetSupportedFrameRatesForResolution(cameraResolution).OrderByDescending((fps) => fps).First();
    //    Debug.Log(cameraFramerate);

    //    UnityEngine.Windows.WebCam.VideoCapture.CreateAsync(false, delegate (UnityEngine.Windows.WebCam.VideoCapture videoCapture)
    //    {
    //        Debug.Log("Entered VideoCapture.CreateAsync call.");

    //        if (videoCapture == null)
    //        {
    //            Debug.Log("EROR: videoCapture instance is null.");

    //        }

    //        if (videoCapture != null)
    //        {
    //            m_VideoCapture = videoCapture;
    //            Debug.Log("Created VideoCapture Instance!");

    //            UnityEngine.Windows.WebCam.CameraParameters cameraParameters = new UnityEngine.Windows.WebCam.CameraParameters();
    //            cameraParameters.hologramOpacity = 0.0f;
    //            cameraParameters.frameRate = cameraFramerate;
    //            cameraParameters.cameraResolutionWidth = cameraResolution.width;
    //            cameraParameters.cameraResolutionHeight = cameraResolution.height;
    //            cameraParameters.pixelFormat = UnityEngine.Windows.WebCam.CapturePixelFormat.BGRA32;

    //            m_VideoCapture.StartVideoModeAsync(cameraParameters,
    //                                               UnityEngine.Windows.WebCam.VideoCapture.AudioState.ApplicationAndMicAudio,
    //                                               OnStartedVideoCaptureMode);
    //        }
    //        else
    //        {
    //            Debug.LogError("Failed to create VideoCapture Instance!");
    //        }
    //    });
    //}

    public void EndVideoCapture()
    {
        m_VideoCapture.StopRecordingAsync(OnStoppedRecordingVideo);
    }

    void OnStartedVideoCaptureMode(UnityEngine.Windows.WebCam.VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("Started Video Capture Mode!");
        string timeStamp = Time.time.ToString().Replace(".", "").Replace(":", "");
        string filename = string.Format("TestVideo_{0}.mp4", timeStamp);
        string filepath = System.IO.Path.Combine(Application.persistentDataPath, filename);
        filepath = filepath.Replace("/", @"\");
        m_VideoCapture.StartRecordingAsync(filepath, OnStartedRecordingVideo);
    }

    void OnStoppedVideoCaptureMode(UnityEngine.Windows.WebCam.VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("Stopped Video Capture Mode!");
    }

    void OnStartedRecordingVideo(UnityEngine.Windows.WebCam.VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("Started Recording Video!");
    }

    void OnStoppedRecordingVideo(UnityEngine.Windows.WebCam.VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("Stopped Recording Video!");
        m_VideoCapture.StopVideoModeAsync(OnStoppedVideoCaptureMode);
    }
}
