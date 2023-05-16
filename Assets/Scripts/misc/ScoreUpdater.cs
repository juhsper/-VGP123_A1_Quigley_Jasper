using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour
{
    public PlayerController playerController;  // Reference to the PlayerController script
    public Text scoreText;  // Reference to the UI Text object to display the score

    private void Update()
    {
        if (playerController != null && scoreText != null)
        {
            // Update the score text with the current score from PlayerController
            scoreText.text = "Score: " + playerController.score.ToString();
        }
    }
}
