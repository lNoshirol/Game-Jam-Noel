using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignPlayer : MonoBehaviour
{
    [SerializeField] Camera cam;
    public int playerID;

    void Start()
    {
        CameraManager.instance.AddCamera(cam);
        PlayerManager.instance.AddPlayer(gameObject);
    }
}
