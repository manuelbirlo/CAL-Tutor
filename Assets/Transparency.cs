using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparency : MonoBehaviour
{
    public GameObject gameObject;
    public Renderer m_Renderer;

    // Start is called before the first frame update
    void Start()
    {
        ChangeAlpha(m_Renderer.material, 0.2f);
    }

    public void MakeObjectTransparent()
    {
        //m_Renderer = gameObject.GetComponent<Renderer>();

        //m_Renderer.material.color = new Color(m_Renderer.material.color.r, m_Renderer.material.color.g, m_Renderer.material.color.b, 0.3f);

        ChangeAlpha(m_Renderer.material, 0.5f);
    }

    private void ChangeAlpha(Material mat, float alphaValue)
    {
        Color oldColor = mat.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaValue);
        mat.SetColor("_Color", newColor);

        //Color color = mat.color;
        //color.a -= 0.1f;
        //mat.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
