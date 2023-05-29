using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the game state, score, and coins.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance.
    /// </summary>
    public static GameManager instance;

    /// <summary>
    /// Number of tiles free from obstacles at the beginning of the game.
    /// </summary>
    public int tilesFreeFromObstacles = 3;

    // Group: Variables to keep track of game state.
    public int thisGameCoins = 0;
    public bool gameStarted = false;
    public bool gameOver = false;
    public bool isPaused = false;

    // Group: Variables to keep track of score and coins.
    private int coins;
    private int personalBest;
    private int pastPersonalBest;
    private bool newPersonalBest = false;
    public int score = 0;

    // Group: Game objects in the scene.
    [SerializeField] GameObject scoreGameObject;
    [SerializeField] GameObject bestScoreGameObject;
    [SerializeField] GameObject coinsGameObject;
    [SerializeField] GameObject startGame;
    [SerializeField] GameObject purchasePanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject personalBestGameObject;

    // Group: Text objects in the scene.
    [SerializeField] Text scoreText;
    [SerializeField] Text personalBestText;
    [SerializeField] Text coinText;
    [SerializeField] Text thisGameCoinsText;
    [SerializeField] Text thisGameScoreText;

    /// <summary>
    /// Reference to the PlayerController component.
    /// </summary>
    [SerializeField] PlayerController playerController;

    private GameUI gameUI;

    /// <summary>
    /// Set up singleton instance
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Time.timeScale = 1f;

        gameUI = FindObjectOfType<GameUI>();

        // Get the personal best score and total coins from PlayerPrefs
        personalBest = PlayerPrefs.GetInt("PersonalBest", 0);
        pastPersonalBest = personalBest;
        coins = PlayerPrefs.GetInt("Coins", 0);

        // Update the personal best text and coins text
        personalBestText.text = personalBest.ToString();
        coinText.text = coins.ToString();

        // only the total coins are visible at start
        scoreGameObject.SetActive(false);
        bestScoreGameObject.SetActive(false);
        coinsGameObject.SetActive(true);

        // activate the purchase panel and the 'press P to play'
        purchasePanel.SetActive(true);
        startGame.SetActive(true);
    }

    private void Update()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);

        coinText.text = coins.ToString();

        if (!gameStarted && (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Return)))
        {
            gameStarted = true;
            startGame.SetActive(false);
            purchasePanel.SetActive(false);
            scoreGameObject.SetActive(true);
            bestScoreGameObject.SetActive(true);
        }

        if (gameOver)
        {
            // Hide game objects and show game over panel
            purchasePanel.SetActive(false);
            scoreGameObject.SetActive(false);
            bestScoreGameObject.SetActive(false);
            coinsGameObject.SetActive(true);

            gameOverPanel.SetActive(true);
            thisGameCoinsText.text = thisGameCoins.ToString();
            thisGameScoreText.text = score.ToString();

            // If the player beat their personal best score, show a message
            if (score > pastPersonalBest)
            {
                personalBestGameObject.SetActive(true);
            }
        }

        // Check for the pause input
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver && gameStarted)
        {
            if (isPaused)
                gameUI.ResumeGame();
            else
                gameUI.PauseGame();
        }
    }

    /// <summary>
    /// Increments the score by the specified amount and updates the score text.
    /// </summary>
    /// <param name="amount">The amount to increment the score.</param>
    public void IncrementScore(int amount)
    {
        // Increase the score and update the score text
        score += amount;
        scoreText.text = score.ToString();

        // If the player beat their personal best score, update the personal best text
        if (score > personalBest)
        {
            personalBest = score;
            PlayerPrefs.SetInt("PersonalBest", personalBest);
            personalBestText.text = personalBest.ToString();

            if (!newPersonalBest)
            {
                gameUI.PlayNewPersonalBest();
                newPersonalBest = true;
            }
        }
    }

    /// <summary>
    /// Increments the coins by the specified amount, updates the coins text, and increases the player's speed.
    /// </summary>
    /// <param name="amount">The amount to increment the coins.</param>
    public void IncrementCoins(int amount)
    {
        // Increase the coins and update the coins text
        thisGameCoins += amount;
        coins += amount;
        PlayerPrefs.SetInt("Coins", coins);
        coinText.text = coins.ToString();

        // Increase the player's speed
        playerController.forwardSpeed += playerController.speedIncreasePerPoint;
    }
}
