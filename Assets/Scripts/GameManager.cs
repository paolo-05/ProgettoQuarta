// import of necessary packages
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // singleton instance

    // Number of tiles free from obstacles at the beginning of the game
    public int tilesFreeFromObstacles = 3;

    // Variables to keep track of game state
    public int thisGameCoins = 0;
    public bool gameStarted = false;
    public bool gameOver = false;

    // Variables to keep track of score and coins
    private int coins;
    private int personalBest;
    private int pastPersonalBest;
    public int score = 0;

    // Game objects in the scene
    [SerializeField] GameObject scoreGameObject;
    [SerializeField] GameObject bestScoreGameObject;
    [SerializeField] GameObject coinsGameObject;
    [SerializeField] GameObject startGame;
    [SerializeField] GameObject purchasePanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject personalBestGameObject;

    // Text objects in the scene
    [SerializeField] Text scoreText;
    [SerializeField] Text personalBestText;
    [SerializeField] Text coinText;
    [SerializeField] Text thisGameCoinsText;
    [SerializeField] Text thisGameScoreText;

    [SerializeField] PlayerController playerController;

    private void Awake()
    {
        // Set up singleton instance
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
        // reset the best score and coins
        // PlayerPrefs.SetInt("PersonalBest", 0);
        PlayerPrefs.SetInt("Coins", 1000);

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

        // activate the purchase panel and the 'press p to play'
        purchasePanel.SetActive(true);
        startGame.SetActive(true);
    }

    private void Update()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);

        coinText.text = coins.ToString();

        if (!gameStarted && Input.GetKeyDown(KeyCode.P))
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
    }

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
        }
    }

    public void IncrementCoins(int amount)
    {
        // Increase the coins and update the coins text
        thisGameCoins += amount;
        coins += amount;
        PlayerPrefs.SetInt("Coins", coins);
        coinText.text = coins.ToString();

        // increase the player speed
        playerController.forwardSpeed += playerController.speedIncreasePerPoint;
    }
}