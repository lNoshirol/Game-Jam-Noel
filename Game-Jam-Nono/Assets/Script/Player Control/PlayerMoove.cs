using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoove : NetworkBehaviour
{
    [SerializeField] float _playerMoveSpeed;

    [SerializeField] Vector3 direction;

    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
        }
        direction = new Vector3(callbackContext.ReadValue<Vector2>().x, 0, callbackContext.ReadValue<Vector2>().y);
    }

    private void Update()
    {
        if (!IsOwner) { return; }
        transform.Translate(_playerMoveSpeed * Time.deltaTime * direction);
    }
}
