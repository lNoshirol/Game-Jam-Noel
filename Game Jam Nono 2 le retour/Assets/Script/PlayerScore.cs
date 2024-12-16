using FishNet.Object;
using UnityEngine;

public class PlayerScore : NetworkBehaviour
{
    public int points = 0;
    public int ownerID;  // Player's unique ID

    // Add points to the player's score
    public void AddPoints()
    {
        points += 1;
        Debug.Log($"Player {ownerID} has {points} points.");
    }
}