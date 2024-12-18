using UnityEngine;
using FishNet.Object;

public class GameLobbyStarter : NetworkBehaviour
{
    [SerializeField] private GameLobbyManager _gameManager;
    [SerializeField] float _countDown;

    [Server]
    private void Start()
    {
        // Start a countdown of 3 minutes (180 seconds)
        _gameManager.StartCountdown(_countDown);
    }
}