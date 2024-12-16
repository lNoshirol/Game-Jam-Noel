using FishNet.Object;
using UnityEngine;

public class TrashZone : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash"))
        {
            Trash trash = other.GetComponent<Trash>();
            if (trash != null)
            {
                // Deactivate the trash
                trash.gameObject.SetActive(false);

                PlayerScore playerPoints = trash.GetOwnerPoints();
                if (playerPoints != null)
                {
                    playerPoints.AddPoints(); // Add points to the player
                    TeamManager.Instance.AddScoreToTeamServer(trash.ownerTag, 1); ; // Add points to the team
                    Debug.Log($"Player {playerPoints.ownerID} scored!");
                }
            }
        }
    }
}
