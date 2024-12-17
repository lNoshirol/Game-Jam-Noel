using UnityEngine;
using FishNet.Object;

public class GameStarter : NetworkBehaviour
{
    [SerializeField] private GameManager _gameManager;

    [Server]
    private void Start()
    {
        // Start a countdown of 3 minutes (180 seconds)
        _gameManager.StartCountdown(180f);
    }
}