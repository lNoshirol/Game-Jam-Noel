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

                PlayerSetUp playerPoints = trash.GetOwnerPoints();
                if (playerPoints != null)
                {
                    

                    if(gameObject.tag == "RaccoonTrashZone")
                    {
                        TeamManager.Instance.AddScoreToTeamServer("Raccoon", 1);
                        if (trash.ownerTag == "Raccoon")
                        {
                            playerPoints.AddPoints(1); // Add points to the player
                        }
                        else
                        {
                            playerPoints.AddPoints(2);
                        }
                        
                    }
                    else 
                    {
                        TeamManager.Instance.AddScoreToTeamServer("Eboueur", 1);
                        if(trash.ownerTag == "Eboueur")
                        {
                            playerPoints.AddPoints(1);
                        }
                        else
                        {
                            playerPoints.AddPoints(2);
                        }
                    }
                    // Add points to the team
                    Debug.Log($"Player {playerPoints.ownerID} scored!");

                }
            }
        }
    }
}
