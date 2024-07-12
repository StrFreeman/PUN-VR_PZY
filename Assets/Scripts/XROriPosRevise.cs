using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XROriPosRevise : MonoBehaviour
{
    public Transform cameraPosTf;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad < 3)   revise();


    }

    void revise()
    {
        Vector3 dif = this.transform.Find("Camera Offset").Find("Main Camera").position - cameraPosTf.position;
        this.transform.position = this.transform.position - dif;
    }
}
