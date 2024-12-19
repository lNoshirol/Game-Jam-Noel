using FishNet.Object;
using UnityEngine;
using TMPro;

public class TeamManager : NetworkBehaviour
{
    public static TeamManager Instance;

    [Header("UI References")]
    public TextMeshProUGUI racconScoreText;
    public TextMeshProUGUI eboueurScoreText;

    private int raccoonScore = 0;
    private int eboueurScore = 0;

    public string hexCodeWinner;
    public GameObject egality;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    [ServerRpc(RequireOwnership = false)]
    public void AddScoreToTeamServer(string teamTag, int points)
    {
        // Update the score based on team tag
        if (teamTag == "Raccoon")
            raccoonScore += points;
        else if (teamTag == "Eboueur")
            eboueurScore += points;

        // Sync the updated scores to all clients
        UpdateTeamScoresClientRpc(raccoonScore, eboueurScore);
    }

    [ObserversRpc]
    private void UpdateTeamScoresClientRpc(int updatedRaccoonScore, int updatedEboueurScore)
    {
        // Update the scores on all clients
        raccoonScore = updatedRaccoonScore;
        eboueurScore = updatedEboueurScore;

        UpdateScoreUI();
    }

    [ServerRpc]
    public void SetWinner()
    {
        if (raccoonScore > eboueurScore)
        {
            racconScoreText.color = ConvertHexToColor(hexCodeWinner);
        }
        else if (eboueurScore > raccoonScore)
        {
            eboueurScoreText.color = ConvertHexToColor(hexCodeWinner);
        }
        else
        {
            egality.SetActive(true);
        }

        SetWinnerClient();
    }

    [ObserversRpc]
    private void SetWinnerClient()
    {
        if (raccoonScore > eboueurScore)
        {
            racconScoreText.color = ConvertHexToColor(hexCodeWinner);
        }
        else if (eboueurScore > raccoonScore)
        {
            eboueurScoreText.color = ConvertHexToColor(hexCodeWinner);
        }
        else
        {
            egality.SetActive(true);
        }

        AndTheWInnerIs();
    }

    public void AndTheWInnerIs()
    {
        if (raccoonScore > eboueurScore)
        {
            racconScoreText.color = ConvertHexToColor(hexCodeWinner);
        }
        else if (eboueurScore > raccoonScore)
        {
            eboueurScoreText.color = ConvertHexToColor(hexCodeWinner);
        }
        else
        {
            egality.SetActive(true);
        }
    }

    private void UpdateScoreUI()
    {
        // Check the local player's team tag to display the correct score
        string localTeamTag = gameObject.tag;

        //if (localTeamTag == "Raccoon")
        //{
            racconScoreText.text = raccoonScore.ToString();
        //}
        //else if (localTeamTag == "Eboueur")
        //{
            eboueurScoreText.text = eboueurScore.ToString();
        //}
    }


    public Color ConvertHexToColor(string hex)
    {
        int Red = int.Parse(hex[0].ToString() + hex[1], System.Globalization.NumberStyles.HexNumber);
        int Blue = int.Parse(hex[2].ToString() + hex[3], System.Globalization.NumberStyles.HexNumber);
        int Green = int.Parse(hex[4].ToString() + hex[5], System.Globalization.NumberStyles.HexNumber);

        return new Color(Red, Blue, Green);
    }
}