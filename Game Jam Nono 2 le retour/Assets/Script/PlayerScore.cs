using FishNet.Demo.AdditiveScenes;
using FishNet.Object;
using UnityEngine;

public class PlayerScore : NetworkBehaviour
{
    public int points = 0;
    public int ownerID;  // Player's unique ID

    public int score; // Player's individual score

    [SerializeField] Material racconMat;
    [SerializeField] Material eboueurMat;
    [SerializeField] GameObject body;

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
        Material teamMaterial;

        if (Owner.ClientId % 2 == 0)
        {
            teamTag = "Raccoon";
            teamMaterial = racconMat;
        }
        else
        {
            teamTag = "Eboueur";
            teamMaterial = eboueurMat;
        }

        // Assign tag and material on the server
        gameObject.tag = teamTag;
        body.GetComponent<MeshRenderer>().material = teamMaterial;

        // Notify all clients to update their visuals
        UpdateTeamOnClients(teamTag, teamMaterial.name);
    }

    [ObserversRpc]
    private void UpdateTeamOnClients(string teamTag, string materialName)
    {
        // Update the tag
        gameObject.tag = teamTag;

        // Retrieve material from MaterialManager
        Material clientMaterial = MaterialManager.Instance.GetMaterialByName(materialName);
        if (clientMaterial != null)
        {
            body.GetComponent<MeshRenderer>().material = clientMaterial;
        }
        else
        {
            Debug.LogError($"Material {materialName} not found on client!");
        }
    }



    // Add points to the player's score
    public void AddPoints()
    {
        points += 1;
        Debug.Log($"Player {ownerID} has {points} points.");
    }
}