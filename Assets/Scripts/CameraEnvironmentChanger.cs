using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEnvironmentChanger : MonoBehaviour
{
    public float SurfaceLevel = 19;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > SurfaceLevel) {
            cam.clearFlags = CameraClearFlags.Skybox;
        } else {
            cam.clearFlags = CameraClearFlags.SolidColor;
        }
    }
}
