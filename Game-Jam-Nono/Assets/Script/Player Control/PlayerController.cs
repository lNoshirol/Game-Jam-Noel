using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehaviour
{
    public bool grabTrash = false;
    [SerializeField] float _playerMoveSpeed;

    [SerializeField] Vector3 direction;
    [SerializeField] float throwForce;
    [SerializeField] GameObject trashStock;

    private void Start()
    {
    }
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

    public void OnThrow(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started && trashStock.transform.childCount > 0)
        {
            Rigidbody trashRB = trashStock.transform.GetChild(0).GetComponent<Rigidbody>();
            trashRB.constraints = RigidbodyConstraints.None;
            trashRB.velocity = direction * 10;
            trashStock.transform.DetachChildren();
            grabTrash = false;
        }
    }
}
