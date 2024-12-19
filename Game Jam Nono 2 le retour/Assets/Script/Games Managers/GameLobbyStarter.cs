using UnityEngine;
using FishNet.Object;

public class GameLobbyStarter : NetworkBehaviour
{
    [SerializeField] private GameLobbyManager _gameManager;
    public float _countDown;

    [Server]
    public void Start()
    {
        _gameManager.StartCountdown(_countDown);
    }
}