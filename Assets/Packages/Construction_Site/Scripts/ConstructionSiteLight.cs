using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSiteLight : MonoBehaviour {
//-------------------------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------------------------
    
    public bool isHDRP = false;

    public float maxBrightness = 1.0f;
    Renderer objRenderer;    
    float delay;
    int mode;
    float brightness;
    Color color;
    float value;

//-------------------------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------------------------
void Start()
{
    objRenderer = GetComponent<Renderer>();        
    delay = Random.Range(0.1f, 2.1f);
    mode = 0;
    brightness = 0.0f;
    value = brightness * 5.0f * maxBrightness;
    color = new Color(value, value, value, 0.0f); 
    if(isHDRP)
        objRenderer.materials[0].SetColor("_EmissiveColor", color * 10.0f);           
    else
        objRenderer.materials[0].SetColor("_EmissionColor", color);
}
//-------------------------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------------------------
void Update()    
{
    //-----------------------------------------------------------------------------------
    // light is off
    if(mode == 0)
    {
        delay -= Time.deltaTime;
        if(delay < 0.0f)
        {
            brightness = 0.0f;
            mode = 1;
        }
    }
    //-----------------------------------------------------------------------------------
    // flash on
    else if(mode == 1)
    {
        brightness += Time.deltaTime * 15.0f;
        if(brightness >= 1.0f)
        {
            brightness = 1.0f;
            delay = 0.05f;
            mode = 2;
        }
        value = brightness * 5.0f * maxBrightness;
        color.r = value;
        color.g = value;
        color.b = value;
        if(isHDRP)
            objRenderer.materials[0].SetColor("_EmissiveColor", color * 10.0f);           
        else
            objRenderer.materials[0].SetColor("_EmissionColor", color);
    }
    //-----------------------------------------------------------------------------------
    // stay on
    else if(mode == 2)
    {
        delay -= Time.deltaTime;
        if(delay < 0.0f)
        {
            mode = 3;
        }    
    }
    //-----------------------------------------------------------------------------------
    // fade off
    else
    {
        brightness -= Time.deltaTime * 2.2f;
        if(brightness <= 0.0f)
        {
            brightness = 0.0f;
            delay = Random.Range(1.9f, 2.2f);
            mode = 0;
        }
        value = brightness * 5.0f * maxBrightness;
        color.r = value;
        color.g = value;
        color.b = value;
        if(isHDRP)
            objRenderer.materials[0].SetColor("_EmissiveColor", color * 10.0f);           
        else
            objRenderer.materials[0].SetColor("_EmissionColor", color);
    }
    //-----------------------------------------------------------------------------------
        
}
//-------------------------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------------------------
}
