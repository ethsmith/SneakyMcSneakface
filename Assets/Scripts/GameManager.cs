using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    // Instance variable
    public static GameManager instance;

    // The menu canvas
    public GameObject menu;
    // The stats canvas for later use
    public GameObject stats;

    // The play button for the menu canvas
    public Button play;
    // The text element for the lives counter
    public Text lives;

    // The player pawn that controls game object details
    public PlayerPawn player;
    // The enemy pawn that controls game object details
    public AIPawn enemy;

    // The max number of lives the player has (to be implemented)
    public int maxLives;

    // First init function
    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        // Show the play menu
        ShowMenu();
        // Listen for clicks on the play buttonand delegate to the initgame function
        play.onClick.AddListener(delegate { InitGame(); });
	}

    // The show menu function
    void ShowMenu() {
        // Show the canvas that is for the main menu
        menu.SetActive(true);
    }

    // The hide menu function
    void HideMenu() {
        // Hide the canvas that is for the main menu
        menu.SetActive(false);
    }

    void InitGame() {
        // Reset all variables
        lives.text = lives.text.Split(':')[0] + ": " + maxLives.ToString();
        // Turn off menu
        HideMenu();
        // Turn on game
        SpawnPlayer();
        SpawnEnemy();
        stats.SetActive(true);
    }

    void SpawnPlayer() {
        // Add the player to the world
        player.gameObject.SetActive(true);
    }

    void SpawnEnemy() {
        // Add the enemy to the world
        enemy.gameObject.SetActive(true);
    }
}
