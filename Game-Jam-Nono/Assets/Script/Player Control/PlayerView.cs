using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class PlayerView : NetworkBehaviour
{
    [SerializeField] GameObject _jspCommentLappeler;

    [SerializeField] Vector3 _direction;

    public void OnLook(InputAction.CallbackContext callbackContext)
    {
        Vector2 value = callbackContext.ReadValue<Vector2>();

        if (value == Vector2.zero) { return; }

        _direction = new Vector3(value.x, 0, value.y);
        
    }

    private void Update()
    {

        Vector3 _lookHere = _jspCommentLappeler.transform.position + _direction;
        _jspCommentLappeler.transform.LookAt(_lookHere);
    }
}
