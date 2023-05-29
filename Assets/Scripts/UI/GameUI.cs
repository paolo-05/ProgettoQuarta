using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Manages the user interface (UI) of the game.
/// </summary>
public class GameUI : MonoBehaviour
{
    /// <summary>
    /// Game object representing the coins value.
    /// </summary>
    [SerializeField] GameObject coinsGameObject;

    /// <summary>
    /// Game object representing best score.
    /// </summary>
    [SerializeField] GameObject bestScoreGameObject;

    /// <summary>
    /// Game object representing current score.
    /// </summary>
    [SerializeField] GameObject scoreGameObject;

    /// <summary>
    /// Game object representing the start game button.
    /// </summary>
    [SerializeField] GameObject startGame;

    /// <summary>
    /// Game object representing the purchase panel
    /// </summary>
    [SerializeField] GameObject purchasePanel;

    /// <summary>
    /// Game object representing the settings panel.
    /// </summary>
    [SerializeField] GameObject settingsPanel;

    /// <summary>
    /// Game object representing the pause panel.
    /// </summary>
    [SerializeField] GameObject pausePanel;

    /// <summary>
    /// Game object representing the pause button.
    /// </summary>
    [SerializeField] GameObject pauseButton;

    /// <summary>
    /// Game object representing the main menu button. 
    /// </summary>
    [SerializeField] GameObject mainMenuButton;

    /// <summary>
    /// Game object representing the help panel.
    /// </summary>
    [SerializeField] GameObject helpPanel;

    /// <summary>
    /// Game object representing the help button.
    /// </summary>
    [SerializeField] GameObject helpButton;

    /// <summary>
    /// Modal dialog object
    /// </summary>
    [SerializeField] ModalDialog alert;

    /// <summary>
    /// Text object for countdown
    /// </summary>
    [SerializeField] Text countdownText;

    [SerializeField] Animator newPersonalBestAnimator;

    /// <summary>
    /// Activated when the object is initialized.
    /// </summary>
    private void Start()
    {
        pauseButton.SetActive(false);
        mainMenuButton.SetActive(true);
        helpButton.SetActive(true);
    }

    /// <summary>
    /// Called every frame.
    /// </summary>
    private void Update()
    {
        if (GameManager.instance.gameStarted)
        {
            mainMenuButton.SetActive(false);
            helpButton.SetActive(false);
            helpPanel.SetActive(false);
            settingsPanel.SetActive(false);
            pauseButton.SetActive(true);
        }
        if (GameManager.instance.isPaused || GameManager.instance.gameOver)
        {
            pauseButton.SetActive(false);
        }
        if (GameManager.instance.gameOver)
        {
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Return))
                PlayAgain();
            else if (Input.GetKeyDown(KeyCode.Escape))
                MainMenu();
        }
    }

    /// <summary>
    /// Starts the game again.
    /// </summary>
    public void PlayAgain()
    {
        Click();

        // Load scene with index 1 (the game scene)
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Goes back to the main menu.
    /// </summary>
    public void MainMenu()
    {
        Click();
        alert.ShowDialog();
    }

    /// <summary>
    /// Opens the settings panel.
    /// </summary>
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

    /// <summary>
    /// Closes the settings panel.
    /// </summary>
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

    /// <summary>
    /// Opens the help panel.
    /// </summary>
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

    /// <summary>
    /// Closes the help panel.
    /// </summary>
    public void CloseHelp()
    {
        Click();

        bestScoreGameObject.SetActive(false);
        scoreGameObject.SetActive(false);
        helpPanel.SetActive(false);

        helpButton.SetActive(true);
    }

    /// <summary>
    /// Pauses the game.
    /// </summary>
    public void PauseGame()
    {
        Click();
        pauseButton.SetActive(false);
        Time.timeScale = 0f; // Stop the game time
        GameManager.instance.isPaused = true;
        pausePanel.SetActive(true);
    }

    /// <summary>
    /// Resumes the game after a countdown.
    /// </summary>
    public void ResumeGame()
    {
        Click();
        pausePanel.SetActive(false);
        StartCoroutine(CountdownCoroutine());
    }

    /// <summary>
    /// Coroutine for the countdown before resuming the game.
    /// </summary>
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

    public void PlayNewPersonalBest()
    {
        newPersonalBestAnimator.SetTrigger("newBest");
    }

    /// <summary>
    /// Plays a button click sound and handles related actions.
    /// </summary>
    private void Click()
    {
        // Play the sound
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }
}
