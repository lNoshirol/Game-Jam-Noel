using UnityEngine;
using TMPro;
using FishNet.Object;  // If you're using TextMeshPro, otherwise use UnityEngine.UI;

public class PlayerUIManager : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI playerScoreText;  // Reference to the UI Text component
    private PlayerSetUp playerPoints;

    public override void OnStartClient()
    {
        if (!base.IsOwner) // Ensure the UI is only active for the owning player
        {
            playerScoreText.gameObject.SetActive(false); // Hide the UI for other players
        }
    }

    void Start()
    {

        playerPoints = GetComponent<PlayerSetUp>();
        if (playerScoreText != null && playerPoints != null)
        {
            UpdateScoreText();
        }
        // Get the PlayerPoints script attached to the player
        playerPoints = GetComponent<PlayerSetUp>();

        if (playerScoreText != null && playerPoints != null)
        {
            // Update the score text immediately when the game starts
            UpdateScoreText();
        }
    }

    void Update()
    {
        // Continuously update the UI to reflect the player's current score
        if (playerPoints != null && playerScoreText != null)
        {
            UpdateScoreText();
        }
    }

    // This function updates the UI text with the player's current score
    private void UpdateScoreText()
    {
        playerScoreText.text = "Score: " + playerPoints.points;
    }
}