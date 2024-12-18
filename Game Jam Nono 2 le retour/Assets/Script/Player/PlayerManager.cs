using FishNet.Object;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
    public static PlayerManager instance;

    // Dictionary to store players by ClientID
    private Dictionary<int, Player> _players = new Dictionary<int, Player>();
    [SerializeField] private List<Player> _playersList = new List<Player>();

    [SerializeField] Material racconMat;
    [SerializeField] Material eboueurMat;

    private void Awake()
    {
        // Set the instance to this script on the server
        if (IsServerInitialized)
        {
            instance = this;
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        
        if (!IsServerInitialized)
        {
            enabled = false; // Disable this script on clients since the server manages the logic
            return;
        }
    }

    /// <summary>
    /// Ensures that the player is initialized when they first connect.
    /// </summary>
    [Server]
    public static void InitializeNewPlayer(int clientID)
    {
        if (!instance._players.ContainsKey(clientID))
        {
            Player newPlayer = new Player
            {
                ClientID = clientID,
                Score = 0
            };

            instance._playersList.Add(newPlayer);

            instance._players.Add(clientID, newPlayer);
            Debug.Log($"[Server] Player {clientID} initialized with a score of 0.");
        }
        else
        {
            Debug.Log($"[Server] Player {clientID} is already initialized.");
        }
    }

    /// <summary>
    /// Adds points to the specified player's score, ensuring they exist first.
    /// </summary>
    [Server]
    public static void PlayerGain(int clientID, int points)
    {
        if (instance == null)
        {
            Debug.LogError("[Server] PlayerManager instance is null.");
            return;
        }

        // Ensure player is initialized before modifying their score
        if (instance._players.ContainsKey(clientID))
        {
            Player player = instance._players[clientID];
            player.Score += points;
            Debug.Log($"[Server] Player {clientID} gained {points} points. Total Score: {player.Score}");

            // Notify all clients of the updated score
            instance.UpdatePlayerScoreObserversRpc(clientID, player.Score);
        }
        else
        {
            Debug.LogError($"[Server] Player {clientID} not found in the PlayerManager.");
        }
    }

    /// <summary>
    /// Updates the score for all clients (this is executed on clients).
    /// </summary>
    [ObserversRpc]
    private void UpdatePlayerScoreObserversRpc(int clientID, int newScore)
    {
        Debug.Log($"[Client] Player {clientID} score updated to {newScore}");
    }

    void AssignTeam(GameObject player)
    {
        if (player.GetComponent<NetworkObject>().Owner.ClientId % 2 == 0)
        {
            player.tag = "Raccoon";
            player.GetComponent<MeshRenderer>().material = racconMat;
        }
        else
        {
            player.tag = "Eboueur";
            player.GetComponent<MeshRenderer>().material = eboueurMat;
        }
            
    }

    // Player data structure
    private class Player
    {
        public int ClientID;
        public int Score;
    }
}
