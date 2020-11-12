using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    // Higher number causes faster spawning
    public int difficulty;

    // Instantiate variables for GameManager and button
    private Button button;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // Get a reference to the GameManager script
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        // Get a reference to the button
        button = GetComponent<Button>();
        // Add an event listener to the button to start the game with the buttons difficulty
        button.onClick.AddListener(SetDifficulty);
    }

    /// <summary>
    /// Set the difficulty multiplier of the game
    /// </summary>
    void SetDifficulty()
    {
        gameManager.StartGame(difficulty);
    }
}
