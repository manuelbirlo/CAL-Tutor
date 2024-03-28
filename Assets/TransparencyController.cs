using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Rendering;

public class TransparencyController : MonoBehaviour
{
    public Material assignedMaterial;

    public float r = 255f;
    public float g = 255f;
    public float b = 255f;

    public Material material2;

    Color originalColor;

    void Start()
    {
        assignedMaterial = GetComponent<MeshRenderer>().materials[0];
        originalColor = assignedMaterial.GetColor("_Color");
    }

    public void MakeObjectTransparent()
    {
        assignedMaterial.SetColor("_Color", new Color(r, g, b, 0.5f));//new Color(0f, 206f, 209f, 0.5f));
        assignedMaterial.SetFloat("_Mode", 3);
        assignedMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        assignedMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        assignedMaterial.EnableKeyword("_ALPHABLEND_ON");
        assignedMaterial.renderQueue = 3000;
    }

    public void MakeObjectNonTransparent()
    {
        assignedMaterial.SetColor("_Color", originalColor);
        assignedMaterial.SetFloat("_Mode", 1);
    }
}
