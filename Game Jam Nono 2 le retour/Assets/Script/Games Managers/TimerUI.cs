using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countdownText; // Reference to a UI Text element
    [SerializeField] private GameLobbyManager _gameManager;

    private void Update()
    {
        if (_gameManager == null) return;

        float timer = _gameManager.CountdownTime;
        _countdownText.text = FormatTime(timer);
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

