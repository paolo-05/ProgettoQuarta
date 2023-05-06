using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int tilesFreeFromObstacles = 3;
    private int score = 0;
    public int thisGameCoins = 0;
    private int coins;
    private int personalBest;
    public float gameSpeed = 5f;
    public float obstacleSpawnTime = 2f;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI personalBestText;
    public TextMeshProUGUI coinText;

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

        InvokeRepeating("IncreaseGameSpeed", 10f, 10f);
        InvokeRepeating("DecreaseObstacleSpawnTime", 10f, 10f);
    }

    private void IncreaseGameSpeed()
    {
        gameSpeed += 1f;
    }

    private void DecreaseObstacleSpawnTime()
    {
        obstacleSpawnTime -= 0.1f;
        if (obstacleSpawnTime < 0.5f)
        {
            obstacleSpawnTime = 0.5f;
        }
    }

    public void IncrementScore()
    {
        score++;
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
    }
}