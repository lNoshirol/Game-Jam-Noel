using FischlWorks_FogWar;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehaviour
{
    public bool grabTrash = false;
    [SerializeField] float _playerMoveSpeed;

    [SerializeField] Vector3 direction;
    [SerializeField] float throwForce;
    public GameObject trashStock;

    [SerializeField] GameObject racoon;

    private void Start()
    {
        if (!IsOwner) { return; }

        gameObject.transform.position = PLAYERSPAWNINSTANCE.instance.transform.position;

        csFogWar.instance.AddFogRevealer(new csFogWar.FogRevealer(transform, 6, false));

        //csFogWar.instance._FogRevealers[0]._RevealerTransform = transform;
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

        Vector3 _lookHere = transform.position + direction;
        racoon.transform.LookAt(_lookHere);
    }

    public void OnThrow(InputAction.CallbackContext callbackContext)
    {
        if (!IsOwner) { return; }

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
