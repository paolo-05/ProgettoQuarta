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
    public bool gameOver = false;

    private int coins;
    private int personalBest;


    [SerializeField] Text scoreText;
    [SerializeField] Text personalBestText;
    [SerializeField] Text coinText;
    [SerializeField] GameObject startGame;
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject purchasePanel;

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
        // reset the best score and coins
        // PlayerPrefs.SetInt("PersonalBest", 0);
        // PlayerPrefs.SetInt("Coins", 0);

        // Get the personal best score and total coins from PlayerPrefs
        personalBest = PlayerPrefs.GetInt("PersonalBest", 0);
        coins = PlayerPrefs.GetInt("Coins", 0);

        // Update the personal best text and coins text
        personalBestText.text = personalBest.ToString();
        coinText.text = coins.ToString();
    }

    private void Update()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);

        coinText.text = coins.ToString();

        if (!gameStarted && Input.GetKeyDown(KeyCode.P))
        {
            gameStarted = true;
            startGame.SetActive(false);
            purchasePanel.SetActive(false );
            playerController.GetComponent<Transform>().position = new Vector3(0, 1, 5);
        }
    }

    public void IncrementScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();

        if (score > personalBest)
        {
            personalBest = score;
            PlayerPrefs.SetInt("PersonalBest", personalBest);
            personalBestText.text = personalBest.ToString();
        }
    }

    public void IncrementCoins(int amount)
    {
        thisGameCoins += amount;
        coins += amount;
        PlayerPrefs.SetInt("Coins", coins);
        coinText.text = coins.ToString();

        playerController.forwardSpeed += playerController.speedIncreasePerPoint;
    }
}