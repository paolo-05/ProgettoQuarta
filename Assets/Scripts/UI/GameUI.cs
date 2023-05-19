using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Need this to manage scene changes
using System.Collections;

public class GameUI : MonoBehaviour
{

    // Game objects in the scene
    [SerializeField] GameObject coinsGameObject;
    [SerializeField] GameObject bestScoreGameObject;
    [SerializeField] GameObject scoreGameObject;
    [SerializeField] GameObject startGame;
    [SerializeField] GameObject purchasePanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject mainMenuButton;
    [SerializeField] GameObject helpPanel;
    [SerializeField] GameObject helpButton;
    [SerializeField] ModalDialog alert;

    [SerializeField] Text countdownText;

    private void Start()
    {
        pauseButton.SetActive(true);
        mainMenuButton.SetActive(true);
        helpButton.SetActive(true);
    }
    private void Update()
    {
        if (GameManager.instance.gameStarted) 
        {
            mainMenuButton.SetActive(false);
            helpButton.SetActive(false);
            helpPanel.SetActive(false);
            settingsPanel.SetActive(false);
        }
    }

    // Function to start the game again
    public void PlayAgain()
    {
        Click();

        // Load scene with index 1 (the game scene)
        SceneManager.LoadScene(1);
    }

    // Function to go back to the main menu
    public void MainMenu()
    {
        Click();
        alert.ShowDialog();
    }

    public void OpenSettings()
    {
        Click();

        // hide the other elements in UI
        coinsGameObject.SetActive(false);
        startGame.SetActive(false);
        purchasePanel.SetActive(false);
        pausePanel.SetActive(false);

        // open the settings by activating the panel
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        Click();
        coinsGameObject.SetActive(true);
        if (!GameManager.instance.gameStarted) 
        {
            startGame.SetActive(true);
            purchasePanel.SetActive(true);
        }
        else
        {
            pausePanel.SetActive(true);
        }
        // close the settings by de-activating the panel
        settingsPanel.SetActive(false);
    }

    public void OpenHelp()
    {
        Click();

        purchasePanel.SetActive(true);
        coinsGameObject.SetActive(true);
        bestScoreGameObject.SetActive(true);
        scoreGameObject.SetActive(true);
        helpPanel.SetActive(true);

        helpButton.SetActive(false);
    }

    public void CloseHelp()
    {
        Click();

        bestScoreGameObject.SetActive(false);
        scoreGameObject.SetActive(false);
        helpPanel.SetActive(false);

        helpButton.SetActive(true);
    }

    public void PauseGame()
    {
        Click();
        pauseButton.SetActive(false);
        Time.timeScale = 0f; // Stop the game time
        GameManager.instance.isPaused = true;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Click();
        pausePanel.SetActive(false);
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        countdownText.gameObject.SetActive(true);
        countdownText.text = "3";
        yield return new WaitForSecondsRealtime(1f);

        Time.timeScale = .4f;
        countdownText.text = "2";
        yield return new WaitForSecondsRealtime(1f);

        Time.timeScale = .6f;
        countdownText.text = "1";
        yield return new WaitForSecondsRealtime(1f);

        countdownText.gameObject.SetActive(false);
        Time.timeScale = 1f; // Resume the game time
        GameManager.instance.isPaused = false;
        pauseButton.SetActive(true);
    }

    private void Click()
    {
        // Play the sound
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }
}
