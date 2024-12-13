using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using UnityEngine.UIElements;

public class PlayerPickupGuide : NetworkBehaviour
{
    Camera cam;
    bool hasObjectInHand;
    GameObject objInHand;
    Transform worldObjectHandler;
    [SerializeField] LayerMask pickupLayer;
    [SerializeField] Transform pickupPosition;
    KeyCode dropButton = KeyCode.Mouse0;
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!base.IsOwner)
            enabled=false;

        worldObjectHandler = GameObject.FindGameObjectWithTag("WorldObjects").transform;
        cam = Camera.main;
    }


    private void Update()
    {
        if (Input.GetKeyDown(dropButton) && hasObjectInHand)
        {
            Drop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag != "Trash") Debug.Log("HALLLO"); return;

        if (!hasObjectInHand)
        {
            SetObjectInHandServer(collision.transform.gameObject, pickupPosition.position, pickupPosition.rotation, gameObject);
            objInHand = collision.transform.gameObject;
            hasObjectInHand = true;

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

    void Drop()
    {
        if (!hasObjectInHand) return;

        DropObjectServer(objInHand, worldObjectHandler);
        hasObjectInHand = false;
        objInHand = null;

    }

    [ServerRpc(RequireOwnership = false)]
    void DropObjectServer(GameObject obj, Transform worldObjects)
    {
        DropObjectObserver(obj, worldObjects);
    }

    [ObserversRpc]
    void DropObjectObserver(GameObject obj, Transform worldObjects) 
    {
        obj.transform.parent = worldObjects;
        if (obj.GetComponent<Rigidbody>() != null)
            obj.GetComponent<Rigidbody>().isKinematic = false;
    }
}
