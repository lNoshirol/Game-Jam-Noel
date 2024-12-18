using FishNet.Object;
using UnityEngine;

public class PlayerScore : NetworkBehaviour
{
    public int points = 0;
    public int ownerID;  // Player's unique ID

    public int score; // Player's individual score
    [SerializeField] GameObject body;

    // Example materials
    public GameObject raccoonBody;
    public GameObject eboueurBody;
    

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
        GameObject noRig;

        if (Owner.ClientId % 2 == 0)
        {
            teamTag = "Raccoon";
            noRig = eboueurBody;
        }
        else
        {
            teamTag = "Eboueur";
            noRig = raccoonBody;
        }

        // Assign tag and set material color on the server
        gameObject.tag = teamTag;
        noRig.SetActive(false);

        //body.GetComponent<MeshRenderer>().material.color = teamColor;

        // Notify all clients to update their visuals
        UpdateTeamOnClients(teamTag, noRig);
    }

    [ObserversRpc]
    private void UpdateTeamOnClients(string teamTag, GameObject noRig)
    {
        // Update the tag and material color on all clients
        gameObject.tag = teamTag;
        noRig.SetActive(false);
    }

    // Add points to the player's score
    public void AddPoints()
    {
        points += 1;
        Debug.Log($"Player {ownerID} has {points} points.");
    }
}