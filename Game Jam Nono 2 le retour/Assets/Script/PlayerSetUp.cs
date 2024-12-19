using FishNet.Object;
using UnityEngine;
using FishNet.Object.Synchronizing;

public class PlayerSetUp : NetworkBehaviour
{
    public int points = 0;
    public int ownerID; // Player's unique ID

    [SerializeField] private GameObject raccoonBody;
    [SerializeField] private GameObject eboueurBody;
    [SerializeField] private GameObject raccoonObjHolder; // Object holder for raccoon
    [SerializeField] private GameObject eboueurObjHolder; // Object holder for eboueur

    private Transform raccoonSpawnPoint; // Spawn point for Raccoon team
    private Transform eboueurSpawnPoint; // Spawn point for Eboueur team

    private readonly SyncVar<bool> _isRaccoon = new SyncVar<bool>();

    public override void OnStartServer()
    {
        base.OnStartServer();
        FindSpawnPoints(); // Find spawn points dynamically
        AssignTeamAndSpawn();
    }

    private void Awake()
    {
        // Subscribe to OnChange event for SyncVar
        _isRaccoon.OnChange += OnTeamChanged;
    }

    private void OnDestroy()
    {
        // Unsubscribe from OnChange to avoid memory leaks
        _isRaccoon.OnChange -= OnTeamChanged;
    }

    private void FindSpawnPoints()
    {
        // Find spawn points by tag or name
        raccoonSpawnPoint = GameObject.FindGameObjectWithTag("RaccoonSpawn").transform;
        eboueurSpawnPoint = GameObject.FindGameObjectWithTag("EboueurSpawn").transform;

        // If spawn points are not found, log a warning
        if (raccoonSpawnPoint == null) Debug.LogError("Raccoon spawn point not found!");
        if (eboueurSpawnPoint == null) Debug.LogError("Eboueur spawn point not found!");
    }

    [Server]
    private void AssignTeamAndSpawn()
    {
        // Assign team based on server logic
        if (Owner.ClientId % 2 == 0)
        {
            _isRaccoon.Value = true; // Raccoon team
            gameObject.tag = "Raccoon";
            TeleportToSpawn(raccoonSpawnPoint);
        }
        else
        {
            _isRaccoon.Value = false; // Eboueur team
            gameObject.tag = "Eboueur";
            TeleportToSpawn(eboueurSpawnPoint);
        }
    }

    private void TeleportToSpawn(Transform spawnPoint)
    {
        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point is not assigned!");
            return;
        }

        // Set position and rotation on the server
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        // Notify all clients of the new position
        RpcTeleport(transform.position, transform.rotation);
    }

    [ObserversRpc]
    private void RpcTeleport(Vector3 position, Quaternion rotation)
    {
        // Update position and rotation for all clients
        transform.position = position;
        transform.rotation = rotation;
    }

    private void OnTeamChanged(bool previous, bool current, bool asServer)
    {
        // Update visuals when the team changes
        UpdateTeamVisuals(current);
    }

    private void UpdateTeamVisuals(bool isRaccoonTeam)
    {
        if (isRaccoonTeam)
        {
            // Enable Raccoon rig and adjust holder
            raccoonBody.SetActive(true);
            eboueurBody.SetActive(false);
            gameObject.GetComponent<PlayerController>()._body = raccoonBody;

            raccoonObjHolder.SetActive(true);
            eboueurObjHolder.SetActive(false);
            gameObject.GetComponent<PlayerPickUp>().pickupPosition = raccoonObjHolder.transform;
        }
        else
        {
            // Enable Eboueur rig and adjust holder
            raccoonBody.SetActive(false);
            eboueurBody.SetActive(true);
            gameObject.GetComponent<PlayerController>()._body = eboueurBody;

            raccoonObjHolder.SetActive(false);
            eboueurObjHolder.SetActive(true);
            gameObject.GetComponent<PlayerPickUp>().pickupPosition = eboueurObjHolder.transform;
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        // Ensure the correct state is applied for this client
        UpdateTeamVisuals(_isRaccoon.Value);

        Debug.Log($"Player {Owner.ClientId} initialized with tag: {gameObject.tag}");
    }

    // Add points to the player's score
    public void AddPoints(int number)
    {
        points += number;
        Debug.Log($"Player {ownerID} has {points} points.");
    }
}
