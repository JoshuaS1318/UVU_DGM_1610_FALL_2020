using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // List of targets
    public List<GameObject> targets;

    // Game active
    public bool isGameActive;

    // Ui elements
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartButton;

    // Speed enemies spawn at
    private float spawnRate = 1.0f;
    // Set the score to zero
    private int score = 0;


    /// <summary>
    /// Spawns Enemies while the game is active
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnTarget()
    {
        // While the game is active
        while (isGameActive)
        {
            // Wait spawnrate seconds
            yield return new WaitForSeconds(spawnRate);

            // Get a random index of what obstacle to spawn
            int index = Random.Range(0, targets.Count);
            // Spawn the obstacle
            Instantiate(targets[index]);
        }
    }

    /// <summary>
    /// Upadate the game score
    /// </summary>
    /// <param name="scoreToAdd">The number of points to add or remove</param>
    public void UpdateScore(int scoreToAdd)
    {
        // Add to the score
        score += scoreToAdd;
        // Update the score text
        scoreText.text = "Score: " + score;
    }

    /// <summary>
    /// Start the game
    /// </summary>
    /// <param name="difficulty">How fast the enemies will spawn</param>
    public void StartGame(int difficulty)
    {
        // Apply the difficulty multiplier
        spawnRate /= difficulty;

        // Hide the title screen
        titleScreen.SetActive(false);

        // Activate the game
        isGameActive = true;

        // Start spawning enemies
        StartCoroutine("SpawnTarget");

        // Reset the score and update the counter
        score = 0;
        UpdateScore(0);
    }

    /// <summary>
    /// Run at the end of the game and show game over screen
    /// </summary>
    public void GameOver()
    {
        // Show game over screen
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

        // Deactivate the game
        isGameActive = false;
    }

    /// <summary>
    /// Reload the game scene
    /// </summary>
    public void RestartGame()
    {
        // Load the game scene again
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
