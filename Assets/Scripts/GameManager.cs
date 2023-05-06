using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int tilesFreeFromObstacles = 3;
    public int score = 0;
    public int coins = 0;
    public float gameSpeed = 5f;
    public float obstacleSpawnTime = 2f;

    public TextMeshProUGUI scoreText;
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
        score += 1;
        scoreText.text = "Score: " + score.ToString();
    }

    public void IncrementCoins(int amount)
    {
        coins += amount;
        coinText.text = "Coins: " + coins.ToString();
    }
}