using FishNet.Object;
using UnityEngine;

public class TrashZone : NetworkBehaviour
{
    // Detect when trash enters the zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object is a Trash object
        if (other.CompareTag("Trash"))
        {
            Trash trash = other.GetComponent<Trash>();

            if (trash != null)
            {
                PlayerScore playerPoints = trash.GetOwnerPoints();

                if (playerPoints != null)
                {
                    // Award points to the player who owns the trash
                    playerPoints.AddPoints();
                    Debug.Log($"Player {playerPoints.ownerID} gained a point by dropping trash!");

                    // Optionally destroy the trash after it's dropped into the zone
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
