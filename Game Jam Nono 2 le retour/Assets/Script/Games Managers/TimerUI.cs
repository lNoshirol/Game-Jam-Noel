using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countdownText; // Reference to a UI Text element
    public GameLobbyManager _gameLobbyManager;

    private void Update()
    {
        if (_gameLobbyManager == null) return;

        float timer = _gameLobbyManager.CountdownTime;
        _countdownText.text = FormatTime(timer);
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
