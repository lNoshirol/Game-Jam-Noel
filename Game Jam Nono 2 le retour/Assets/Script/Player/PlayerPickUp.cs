using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class PlayerPickUp : NetworkBehaviour
{
    [SerializeField] float raycastDistance;
    [SerializeField] LayerMask pickupLayer;
    [SerializeField] Transform pickupPosition;
    [SerializeField] GameObject body;

    Camera cam;
    bool hasObjectInHand;
    GameObject objInHand;
    Transform objectHolder;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!base.IsOwner)
            enabled = false;

        cam = Camera.main;
        
    }

    public void OnGrab(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.started) { return; }
        
        Grab();
    }

    void Grab()
    {
        // Draw the raycast in the scene view for debugging
        /*Physics.BoxCast(body.transform.forward, new Vector3(1, 1, 3), body.transform.forward);
        Physics.box*/


        if (Physics.Raycast(body.transform.position, body.transform.forward, out RaycastHit hit, raycastDistance, pickupLayer))
        {
            if (!hasObjectInHand)
            {
                SetObjectInHandServer(hit.transform.gameObject, pickupPosition.position, pickupPosition.rotation, gameObject);
                objInHand = hit.transform.gameObject;
                hasObjectInHand = true;
            }
            else if (hasObjectInHand)
            {
                Drop(false);

                SetObjectInHandServer(hit.transform.gameObject, pickupPosition.position, pickupPosition.rotation, gameObject);
                objInHand = hit.transform.gameObject;
                hasObjectInHand = true;
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void SetObjectInHandServer(GameObject obj, Vector3 position, Quaternion rotation, GameObject player)
    {
        SetObjectInHandObserver(obj, position, rotation, player);
    }

    [ObserversRpc]
    void SetObjectInHandObserver(GameObject obj, Vector3 position, Quaternion rotation, GameObject player)
    {
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.transform.parent = player.transform;

        if (obj.GetComponent<Rigidbody>() != null)
            obj.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void OnDrop(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.started) { return; }

        Drop(true);
    }

    void Drop(bool IsThrowing)
    {
        if (!hasObjectInHand)
            return;

        DropObjectServer(objInHand, objectHolder, IsThrowing);
        hasObjectInHand = false;
        objInHand = null;
    }

    [ServerRpc(RequireOwnership = false)]
    void DropObjectServer(GameObject obj, Transform worldHolder, bool IsThrowing)
    {
        DropObjectObserver(obj, worldHolder, IsThrowing);
    }

    [ObserversRpc]
    void DropObjectObserver(GameObject obj, Transform worldHolder, bool IsThrowing)
    {
        obj.transform.parent = worldHolder;

        if (obj.GetComponent<Rigidbody>() != null)
            obj.GetComponent<Rigidbody>().isKinematic = false;

        if (IsThrowing)
        {
            PlayerController _pc = GetComponent<PlayerController>();
            Vector3 LANCE = new Vector3(Mathf.Clamp(_pc.GetDirection().x * 200, -200, 200), 200, Mathf.Clamp(_pc.GetDirection().z * 200, -200, 200));
            obj.GetComponent<Rigidbody>().AddForce(LANCE);
        }
    }
}
