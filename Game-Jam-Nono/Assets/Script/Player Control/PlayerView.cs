using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerView : NetworkBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] Camera _camera;
    [SerializeField] GameObject _cameraHolder;

    private void Update()
    {

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo);
        
        if (hitInfo.collider != null)
        {
            _player.transform.LookAt(new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z));
        }
    }
}
