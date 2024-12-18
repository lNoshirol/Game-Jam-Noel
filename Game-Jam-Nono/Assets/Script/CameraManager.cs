using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [SerializeField] List<Camera> cameras;

    private void Awake()
    {
        instance = this;
    }

    public void AddCamera(Camera cam)
    {
        cameras.Add(cam);
        cam.targetDisplay = cameras.Count;
    }
}
