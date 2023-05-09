using UnityEngine;
using Unity.UI;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int tilesFreeFromObstacles = 3;
    private int score = 0;
    public int thisGameCoins = 0;
    public bool gameStarted = false;

    private int coins;
    private int personalBest;


    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI personalBestText;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] GameObject startGame;

    [SerializeField] PlayerMovement playerMovement;

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
        // Get the personal best score and total coins from PlayerPrefs
        personalBest = PlayerPrefs.GetInt("PersonalBest", 0);
        coins = PlayerPrefs.GetInt("Coins", 0);

        // Update the personal best text and coins text
        personalBestText.text = "Personal Best: " + personalBest.ToString();
        coinText.text = "Coins: " + coins.ToString();
    }

    private void Update()
    {
        if (!gameStarted && Input.anyKeyDown)
        {
            gameStarted = true;
            startGame.SetActive(false);
        }
    }

    public void IncrementScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score.ToString();

        if (score > personalBest)
        {
            personalBest = score;
            PlayerPrefs.SetInt("PersonalBest", personalBest);
            personalBestText.text = "Personal Best: " + personalBest.ToString();
        }
    }

    public void IncrementCoins(int amount)
    {
        thisGameCoins += amount;
        coins += amount;
        PlayerPrefs.SetInt("Coins", coins);
        coinText.text = "Coins: " + coins.ToString();

        playerMovement.speed += playerMovement.speedIncreasePerPoint;
    }
}