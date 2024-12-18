using System.Collections;
using UnityEngine;
using FishNet.Object;

public class OldPlayerPickup : NetworkBehaviour
{
    [SerializeField] float raycastDistance = 5f; // Raycast range
    [SerializeField] LayerMask pickupLayer;
    [SerializeField] Transform pickupPosition;
    [SerializeField] KeyCode pickupButton = KeyCode.Mouse0;
    [SerializeField] KeyCode dropButton = KeyCode.Mouse1;
    [SerializeField] GameObject body;
    [SerializeField] float stunDuration = 10f; // Stun time in seconds

    private bool isStunned = false; // To block actions during stun
    private Camera cam;
    private bool hasObjectInHand;
    private GameObject objInHand;
    private Transform worldObjectHolder;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!base.IsOwner)
            enabled = false;

        cam = Camera.main;
        worldObjectHolder = GameObject.FindGameObjectWithTag("WorldObjects").transform;
    }

    private void Update()
    {
        if (isStunned) return; // Block all input if stunned

        if (Input.GetKeyDown(pickupButton))
            AttemptPickupOrStun();

        if (Input.GetKeyDown(dropButton))
            Drop();
    }

    void AttemptPickupOrStun()
    {
        // Perform the raycast
        if (Physics.Raycast(body.transform.position, body.transform.forward, out RaycastHit hit, raycastDistance, pickupLayer))
        {
            Debug.DrawRay(body.transform.position, body.transform.forward * raycastDistance, Color.red, 1f);

            // Check if the raycast hit another player
            OldPlayerPickup hitPlayer = hit.transform.GetComponent<OldPlayerPickup>();
            if (hitPlayer != null)
            {
                Debug.Log($"Player {hitPlayer.name} is stunned!");
                StunPlayerServer(hitPlayer.gameObject);
                return;
            }

            // Otherwise, handle the pickup
            if (!hasObjectInHand)
            {
                objInHand = hit.transform.gameObject;

                Trash trash = objInHand.GetComponent<Trash>();
                if (trash != null)
                {
                    PlayerScore playerPoints = GetComponent<PlayerScore>();
                    trash.SetOwner(playerPoints); // Associate the trash with the player's score
                    trash.ownerTag = gameObject.tag;
                    Debug.Log($"Player {playerPoints.ownerID} picked up trash.");
                }

                SetObjectInHandServer(objInHand, pickupPosition.position, pickupPosition.rotation, gameObject);
                hasObjectInHand = true;
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void StunPlayerServer(GameObject player)
    {
        StunPlayerObservers(player);
    }

    [ObserversRpc]
    void StunPlayerObservers(GameObject player)
    {
        OldPlayerPickup playerPickup = player.GetComponent<OldPlayerPickup>();
        if (playerPickup != null)
        {
            playerPickup.ApplyStun();
        }
    }

    void ApplyStun()
    {
        if (isStunned) return; // Prevent reapplying the stun

        // Drop the object if the player has one in hand
        if (hasObjectInHand)
        {
            Drop(); // Ensure the item is dropped when stunned
        }

        StartCoroutine(StunRoutine());
    }

    private IEnumerator StunRoutine()
    {
        isStunned = true; // Block player actions
        Debug.Log($"{gameObject.name} is stunned for {stunDuration} seconds!");

        yield return new WaitForSeconds(stunDuration);

        isStunned = false; // Re-enable player actions
        Debug.Log($"{gameObject.name} is no longer stunned!");
    }

    void Drop()
    {
        if (!hasObjectInHand)
            return;

        DropObjectServer(objInHand, worldObjectHolder);
        hasObjectInHand = false;
        objInHand = null;
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

    [ServerRpc(RequireOwnership = false)]
    void DropObjectServer(GameObject obj, Transform worldHolder)
    {
        DropObjectObserver(obj, worldHolder);
    }

    [ObserversRpc]
    void DropObjectObserver(GameObject obj, Transform worldHolder)
    {
        obj.transform.parent = worldHolder;

        if (obj.GetComponent<Rigidbody>() != null)
            obj.GetComponent<Rigidbody>().isKinematic = false;
    }
}