using FishNet.Object;
using UnityEngine;

public class PlayerScore : NetworkBehaviour
{
    public int points = 0;
    public int ownerID;  // Player's unique ID

    public int score; // Player's individual score

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (IsServerInitialized)
        {
            AssignTeam(); // Assign team based on server logic
        }

        Debug.Log($"Player {Owner.ClientId} initialized with tag: {gameObject.tag}");
    }

    [Server]
    private void AssignTeam()
    {
        // Example logic: alternate between teams
        if (Owner.ClientId % 2 == 0)
        {
            gameObject.tag = "Raccoon";
        }
        else
        {
            gameObject.tag = "Eboueur";
        }
    }

    // Add points to the player's score
    public void AddPoints()
    {
        points += 1;
        Debug.Log($"Player {ownerID} has {points} points.");
    }
}