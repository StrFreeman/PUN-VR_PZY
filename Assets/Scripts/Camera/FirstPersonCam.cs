using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float oriYaw = 0.0f;
    private float oriPitch = 0.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles += new Vector3(pitch-oriPitch, yaw-oriYaw, 0.0f);

        oriYaw = yaw;
        oriPitch = pitch;
    }
}
