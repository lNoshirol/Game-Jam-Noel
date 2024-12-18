using FishNet.Object;
using UnityEngine;
using FishNet.Object.Synchronizing;

public class PlayerSetUp : NetworkBehaviour
{
    public int points = 0;
    public int ownerID; // Player's unique ID

    [SerializeField] private GameObject raccoonBody;
    [SerializeField] private GameObject eboueurBody;

    private readonly SyncVar<bool> _isRaccoon = new SyncVar<bool>();

    public override void OnStartServer()
    {
        base.OnStartServer();
        AssignTeam();
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

    [Server]
    private void AssignTeam()
    {
        // Assign team based on server logic
        if (Owner.ClientId % 2 == 0)
        {
            _isRaccoon.Value = true; // Raccoon team
            gameObject.tag = "Raccoon";
        }
        else
        {
            _isRaccoon.Value = false; // Eboueur team
            gameObject.tag = "Eboueur";
        }
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
            raccoonBody.SetActive(true);
            eboueurBody.SetActive(false);
        }
        else
        {
            raccoonBody.SetActive(false);
            eboueurBody.SetActive(true);
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
