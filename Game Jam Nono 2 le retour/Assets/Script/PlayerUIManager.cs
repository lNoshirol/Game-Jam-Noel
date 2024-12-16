using UnityEngine;
using TMPro;  // If you're using TextMeshPro, otherwise use UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerScoreText;  // Reference to the UI Text component
    private PlayerScore playerPoints;

    void Start()
    {

        //if (!IsOwner) // Ensure the UI is only active for the owning player
        //{
        //    gameObject.SetActive(false); // Hide the UI for other players
        //}

        playerPoints = GetComponent<PlayerScore>();
        if (playerScoreText != null && playerPoints != null)
        {
            UpdateScoreText();
        }
        // Get the PlayerPoints script attached to the player
        playerPoints = GetComponent<PlayerScore>();

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