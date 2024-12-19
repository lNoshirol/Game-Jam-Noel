using UnityEngine;
using FishNet.Object;

public class GameLobbyManager : NetworkBehaviour
{
    private float _endTime;
    private bool _isCountingDown = false;

    public GameObject AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAh;
    public GameObject AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAh2;
    public int gameCountDownDuration;

    public GameObject gameManager;

    public bool isInGame;

    public float CountdownTime { get; private set; } // Expose remaining time to clients

    private void Update()
    {
        if (IsServerInitialized && _isCountingDown)
        {
            CountdownTime = Mathf.Max(0, _endTime - Time.time); // Calculate remaining time

            if (CountdownTime <= 0)
            {
                _isCountingDown = false;
                OnCountdownFinished(); // Trigger the event for when the countdown ends
            }

            UpdateClientsTimer(CountdownTime); // Sync with clients
        }
    }


    [Server]
    public void StartCountdown(float duration)
    {
        CountdownTime = duration;
        _endTime = Time.time + duration; // Calculate the end time
        _isCountingDown = true;
        UpdateClientsTimer(CountdownTime); // Initial sync
    }

    [ObserversRpc]
    private void UpdateClientsTimer(float timer)
    {
        CountdownTime = timer; // Sync the timer on clients
    }

    private void OnCountdownFinished()
    {
        if (!isInGame)
        {
            isInGame = true;
            AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAh2.SetActive(true);
            gameManager.SetActive(true);
            GetComponent<GameLobbyStarter>()._countDown = gameCountDownDuration;
            GetComponent<GameLobbyStarter>().Start();
            Debug.Log("Game Start !");
        }
        else
        {
            
            Debug.Log("Game Is End");
        }
        
        /*AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAh2.SetActive(true);
        gameManager.SetActive(true);
        gameObject.SetActive(false);*/
    }
}