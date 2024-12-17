using FishNet.Object;
using UnityEngine;

public class PlayerScore : NetworkBehaviour
{
    public int points = 0;
    public int ownerID;  // Player's unique ID

    public int score; // Player's individual score
    [SerializeField] GameObject body;

    // Example materials
    public Material racconMat;
    public Material eboueurMat;

    private void Start()
    {
        // Set colors manually
        racconMat.color = Color.blue; // Blue
        eboueurMat.color = Color.red; // Red
    }
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
        string teamTag;
        Color teamColor;

        if (Owner.ClientId % 2 == 0)
        {
            teamTag = "Raccoon";
            teamColor = racconMat.color; // Use raccoon material color
        }
        else
        {
            teamTag = "Eboueur";
            teamColor = eboueurMat.color; // Use eboueur material color
        }

        // Assign tag and set material color on the server
        gameObject.tag = teamTag;
        body.GetComponent<MeshRenderer>().material.color = teamColor;

        // Notify all clients to update their visuals
        UpdateTeamOnClients(teamTag, teamColor);
    }

    [ObserversRpc]
    private void UpdateTeamOnClients(string teamTag, Color teamColor)
    {
        // Update the tag and material color on all clients
        gameObject.tag = teamTag;
        body.GetComponent<MeshRenderer>().material.color = teamColor;
    }

    // Add points to the player's score
    public void AddPoints()
    {
        points += 1;
        Debug.Log($"Player {ownerID} has {points} points.");
    }
}