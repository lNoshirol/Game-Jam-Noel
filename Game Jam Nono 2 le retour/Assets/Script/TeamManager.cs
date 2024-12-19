using FishNet.Object;
using UnityEngine;
using TMPro;

public class TeamManager : NetworkBehaviour
{
    public static TeamManager Instance;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI racconScoreText;
    [SerializeField] private TextMeshProUGUI eboueurScoreText;

    private int raccoonScore = 0;
    private int eboueurScore = 0;

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

    private void UpdateScoreUI()
    {
        // Check the local player's team tag to display the correct score
        string localTeamTag = gameObject.tag;

        //if (localTeamTag == "Raccoon")
        //{
            racconScoreText.text = $"Raccon : {raccoonScore}";
        //}
        //else if (localTeamTag == "Eboueur")
        //{
            eboueurScoreText.text = $"Eboueur : {eboueurScore}";
        //}
    }


}