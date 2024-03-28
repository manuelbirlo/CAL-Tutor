using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class PlaneNavigation : MonoBehaviour
{
    public int PlaneNumber;
    public string elapsedTime;

    public GameObject ultrasoundPlaneOfProbe;

    public GameObject targetUltrasoundPlane;

    private Vector3 targetPos;
    Vector3 currentPos;

    private LineRenderer line;

    private Stopwatch stopWatch;

    public List<Vector3> CornerVerticesProbePlane;
    List<Vector3> LocalVerticesProbePlane;
    List<Vector3> GlobalVerticesProbePlane;

    List<Vector3> CornerVerticesTargetPlane;
    List<Vector3> LocalVerticesTargetPlane;
    List<Vector3> GlobalVerticesTargetPlane;

    List<int> CornerIDs = new List<int> { 0, 10, 110, 120 };
    float radius = 0.5f;

    public float PercentHead = 0.7f;

    LineRenderer child1LineRenderer;
    LineRenderer child2LineRenderer;
    LineRenderer child3LineRenderer;
    LineRenderer child4LineRenderer;

    LineRenderer child1LineRendererArrow;
    LineRenderer child2LineRendererArrow;
    LineRenderer child3LineRendererArrow;
    LineRenderer child4LineRendererArrow;

    LineRenderer xCoordinateAxisArrow;
    LineRenderer xCoordinateAxisArrowHead;
    LineRenderer yCoordinateAxisArrow;
    LineRenderer zCoordinateAxisArrow;

    GameObject sphere;

    // Start is called before the first frame update
    void OnEnable()
    {
        stopWatch = new Stopwatch();
        stopWatch.Start();

        Transform parent = ultrasoundPlaneOfProbe.transform;
        GameObject line1 = new GameObject();
        line1.name = "Line1";
        line1.tag = "Line";
        GameObject line2 = new GameObject();
        line2.name = "Line2";
        line2.tag = "Line";
        GameObject line3 = new GameObject();
        line3.name = "Line3";
        line3.tag = "Line";
        GameObject line4 = new GameObject();
        line4.name = "Line4";
        line4.tag = "Line";

        child1LineRenderer = line1.AddComponent<LineRenderer>();
        child1LineRenderer.material.SetColor("_Color", new Color(255f, 0f, 255f, 0.5f));
        child1LineRenderer.material.SetFloat("_Mode", 3);
        child1LineRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        child1LineRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        child1LineRenderer.material.EnableKeyword("_ALPHABLEND_ON");
        child1LineRenderer.material.renderQueue = 3000;

        child1LineRenderer.gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        child2LineRenderer = line2.AddComponent<LineRenderer>();
        child2LineRenderer.material.SetColor("_Color", new Color(255f, 0f, 255f, 0.5f));
        child2LineRenderer.material.SetFloat("_Mode", 3);
        child2LineRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        child2LineRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        child2LineRenderer.material.EnableKeyword("_ALPHABLEND_ON");
        child2LineRenderer.material.renderQueue = 3000;

        child2LineRenderer.gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        child3LineRenderer = line3.AddComponent<LineRenderer>();
        child3LineRenderer.material.SetColor("_Color", new Color(255f, 0f, 255f, 0.5f));
        child3LineRenderer.material.SetFloat("_Mode", 3);
        child3LineRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        child3LineRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        child3LineRenderer.material.EnableKeyword("_ALPHABLEND_ON");
        child3LineRenderer.material.renderQueue = 3000;

        child3LineRenderer.gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        child4LineRenderer = line4.AddComponent<LineRenderer>();
        child4LineRenderer.material.SetColor("_Color", new Color(255f, 0f, 255f, 0.5f));
        child4LineRenderer.material.SetFloat("_Mode", 3);
        child4LineRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        child4LineRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        child4LineRenderer.material.EnableKeyword("_ALPHABLEND_ON");
        child4LineRenderer.material.renderQueue = 3000;

        child4LineRenderer.gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        child1LineRenderer.gameObject.transform.SetParent(parent);
        child1LineRenderer.SetWidth(0.005F, 0.005F);
        child1LineRenderer.SetVertexCount(2);
        child2LineRenderer.gameObject.transform.SetParent(parent);
        child2LineRenderer.SetWidth(0.005F, 0.005F);
        child2LineRenderer.SetVertexCount(2);
        child3LineRenderer.gameObject.transform.SetParent(parent);
        child3LineRenderer.SetWidth(0.005F, 0.005F);
        child3LineRenderer.SetVertexCount(2);
        child4LineRenderer.gameObject.transform.SetParent(parent);
        child4LineRenderer.SetWidth(0.005F, 0.005F);
        child4LineRenderer.SetVertexCount(2);

        GameObject arrow1 = new GameObject();
        arrow1.name = "arrow1";
        arrow1.tag = "Arrow";
        GameObject arrow2 = new GameObject();
        arrow2.name = "arrow2";
        arrow2.tag = "Arrow";
        GameObject arrow3 = new GameObject();
        arrow3.name = "arrow3";
        arrow3.tag = "Arrow";
        GameObject arrow4 = new GameObject();
        arrow4.name = "arrow4";
        arrow4.tag = "Arrow";

        child1LineRendererArrow = arrow1.AddComponent<LineRenderer>();
        child1LineRendererArrow.material.SetColor("_Color", new Color(255f, 0f, 255f, 0.5f));
        child1LineRendererArrow.material.SetFloat("_Mode", 3);
        child1LineRendererArrow.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        child1LineRendererArrow.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        child1LineRendererArrow.material.EnableKeyword("_ALPHABLEND_ON");
        child1LineRendererArrow.material.renderQueue = 3000;

        child2LineRendererArrow = arrow2.AddComponent<LineRenderer>();
        child2LineRendererArrow.material.SetColor("_Color", new Color(255f, 0f, 255f, 0.5f));
        child2LineRendererArrow.material.SetFloat("_Mode", 3);
        child2LineRendererArrow.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        child2LineRendererArrow.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        child2LineRendererArrow.material.EnableKeyword("_ALPHABLEND_ON");
        child2LineRendererArrow.material.renderQueue = 3000;

        child3LineRendererArrow = arrow3.AddComponent<LineRenderer>();
        child3LineRendererArrow.material.SetColor("_Color", new Color(255f, 0f, 255f, 0.5f));
        child3LineRendererArrow.material.SetFloat("_Mode", 3);
        child3LineRendererArrow.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        child3LineRendererArrow.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        child3LineRendererArrow.material.EnableKeyword("_ALPHABLEND_ON");
        child3LineRendererArrow.material.renderQueue = 3000;

        child4LineRendererArrow = arrow4.AddComponent<LineRenderer>();
        child4LineRendererArrow.material.SetColor("_Color", new Color(255f, 0f, 255f, 0.5f));
        child4LineRendererArrow.material.SetFloat("_Mode", 3);
        child4LineRendererArrow.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        child4LineRendererArrow.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        child4LineRendererArrow.material.EnableKeyword("_ALPHABLEND_ON");
        child4LineRendererArrow.material.renderQueue = 3000;

        CornerVerticesProbePlane = new List<Vector3>();
        LocalVerticesProbePlane = new List<Vector3>();
        GlobalVerticesProbePlane = new List<Vector3>();

        CornerVerticesTargetPlane = new List<Vector3>();
        LocalVerticesTargetPlane = new List<Vector3>();
        GlobalVerticesTargetPlane = new List<Vector3>();
    }

    void OnDisable()
    {
        stopWatch = null;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the GameObjects are not null
        if (ultrasoundPlaneOfProbe != null && targetUltrasoundPlane != null)
        {
            GetVertices();

            child1LineRenderer.SetPosition(0, CornerVerticesProbePlane[0]);
            child1LineRenderer.SetPosition(1, CornerVerticesTargetPlane[0]);
            child2LineRenderer.SetPosition(0, CornerVerticesProbePlane[1]);
            child2LineRenderer.SetPosition(1, CornerVerticesTargetPlane[1]);
            child3LineRenderer.SetPosition(0, CornerVerticesProbePlane[2]);
            child3LineRenderer.SetPosition(1, CornerVerticesTargetPlane[2]);
            child4LineRenderer.SetPosition(0, CornerVerticesProbePlane[3]);
            child4LineRenderer.SetPosition(1, CornerVerticesTargetPlane[3]);

            UnityEngine.Debug.Log("4");
           
            PercentHead = 0.7f;

            child1LineRendererArrow.widthCurve = new AnimationCurve(
            new Keyframe(0, 0.1f)
            , new Keyframe(0.999f - PercentHead, 0.3f)//0.3f)  // neck of arrow
            , new Keyframe(1 - PercentHead, 1f) // max width of arrow head
            , new Keyframe(1, 0f));

            child2LineRendererArrow.widthCurve = new AnimationCurve(
            new Keyframe(0, 0.1f)
            , new Keyframe(0.999f - PercentHead, 0.3f)  // neck of arrow
            , new Keyframe(1 - PercentHead, 1f) // max width of arrow head
            , new Keyframe(1, 0f));

            child3LineRendererArrow.widthCurve = new AnimationCurve(
            new Keyframe(0, 0.1f)
            , new Keyframe(0.999f - PercentHead, 0.3f)  // neck of arrow
            , new Keyframe(1 - PercentHead, 1f) // max width of arrow head
            , new Keyframe(1, 0f));

            child4LineRendererArrow.widthCurve = new AnimationCurve(
            new Keyframe(0, 0.1f)
            , new Keyframe(0.999f - PercentHead, 0.3f)  // neck of arrow
            , new Keyframe(1 - PercentHead, 1f) // max width of arrow head
            , new Keyframe(1, 0f));

            var startPositionOfArrow1 = new Vector3();
            var startPositionOfArrow2 = new Vector3();
            var startPositionOfArrow3 = new Vector3();
            var startPositionOfArrow4 = new Vector3();

            startPositionOfArrow1.x = CornerVerticesProbePlane[0].x + (0.95f * (CornerVerticesTargetPlane[0].x - CornerVerticesProbePlane[0].x));
            startPositionOfArrow1.y = CornerVerticesProbePlane[0].y + (0.95f * (CornerVerticesTargetPlane[0].y - CornerVerticesProbePlane[0].y));
            startPositionOfArrow1.z = CornerVerticesProbePlane[0].z + (0.95f * (CornerVerticesTargetPlane[0].z - CornerVerticesProbePlane[0].z));
                                                                   
            startPositionOfArrow2.x = CornerVerticesProbePlane[1].x + (0.95f * (CornerVerticesTargetPlane[1].x - CornerVerticesProbePlane[1].x));
            startPositionOfArrow2.y = CornerVerticesProbePlane[1].y + (0.95f * (CornerVerticesTargetPlane[1].y - CornerVerticesProbePlane[1].y));
            startPositionOfArrow2.z = CornerVerticesProbePlane[1].z + (0.95f * (CornerVerticesTargetPlane[1].z - CornerVerticesProbePlane[1].z));
                                                                   
            startPositionOfArrow3.x = CornerVerticesProbePlane[2].x + (0.95f * (CornerVerticesTargetPlane[2].x - CornerVerticesProbePlane[2].x));
            startPositionOfArrow3.y = CornerVerticesProbePlane[2].y + (0.95f * (CornerVerticesTargetPlane[2].y - CornerVerticesProbePlane[2].y));
            startPositionOfArrow3.z = CornerVerticesProbePlane[2].z + (0.95f * (CornerVerticesTargetPlane[2].z - CornerVerticesProbePlane[2].z));
                                                                   
            startPositionOfArrow4.x = CornerVerticesProbePlane[3].x + (0.95f * (CornerVerticesTargetPlane[3].x - CornerVerticesProbePlane[3].x));
            startPositionOfArrow4.y = CornerVerticesProbePlane[3].y + (0.95f * (CornerVerticesTargetPlane[3].y - CornerVerticesProbePlane[3].y));
            startPositionOfArrow4.z = CornerVerticesProbePlane[3].z + (0.95f * (CornerVerticesTargetPlane[3].z - CornerVerticesProbePlane[3].z));

            child1LineRendererArrow.SetPositions(new Vector3[] {
              startPositionOfArrow1
              , Vector3.Lerp(startPositionOfArrow1, CornerVerticesTargetPlane[0], 1) //0.999f - PercentHead);
              , Vector3.Lerp(startPositionOfArrow1, CornerVerticesTargetPlane[0], 1) //1 - PercentHead)
              , startPositionOfArrow1 });

            child2LineRendererArrow.SetPositions(new Vector3[] {
              startPositionOfArrow2
              , Vector3.Lerp(startPositionOfArrow2, CornerVerticesTargetPlane[1], 1) //0.999f - PercentHead);
              , Vector3.Lerp(startPositionOfArrow2, CornerVerticesTargetPlane[1], 1) //1 - PercentHead)
              , startPositionOfArrow2 });

            child3LineRendererArrow.SetPositions(new Vector3[] {
              startPositionOfArrow3
              , Vector3.Lerp(startPositionOfArrow3, CornerVerticesTargetPlane[2], 1) //0.999f - PercentHead);
              , Vector3.Lerp(startPositionOfArrow3, CornerVerticesTargetPlane[2], 1) //1 - PercentHead)
              , startPositionOfArrow3 });

            child4LineRendererArrow.SetPositions(new Vector3[] {
              startPositionOfArrow4
              , Vector3.Lerp(startPositionOfArrow4, CornerVerticesTargetPlane[3], 1) //0.999f - PercentHead);
              , Vector3.Lerp(startPositionOfArrow4, CornerVerticesTargetPlane[3], 1) //1 - PercentHead)
              , startPositionOfArrow4 });
        }

    }

    void DestroyLine()
    {
        stopWatch.Stop();
        
        Destroy(line);
    }

    void GetVertices()
    {
        UnityEngine.Debug.Log("6");

        // Probe plane
        LocalVerticesProbePlane = new List<Vector3>(ultrasoundPlaneOfProbe.GetComponent<MeshFilter>().mesh.vertices);

        GlobalVerticesProbePlane.Clear();
        CornerVerticesProbePlane.Clear();

        foreach (Vector3 point in LocalVerticesProbePlane)
        {
            GlobalVerticesProbePlane.Add(ultrasoundPlaneOfProbe.transform.TransformPoint(point));
        }

        foreach (int id in CornerIDs)
        {
            CornerVerticesProbePlane.Add(GlobalVerticesProbePlane[id]);
        }

        // Target plane
        LocalVerticesTargetPlane = new List<Vector3>(targetUltrasoundPlane.GetComponent<MeshFilter>().mesh.vertices);
        GlobalVerticesTargetPlane.Clear();

        foreach (Vector3 point in LocalVerticesTargetPlane)
        {
            GlobalVerticesTargetPlane.Add(targetUltrasoundPlane.transform.TransformPoint(point));
        }

        foreach (int id in CornerIDs)
        {
            CornerVerticesTargetPlane.Add(GlobalVerticesTargetPlane[id]);
        }

        UnityEngine.Debug.Log("7");
    }
}
