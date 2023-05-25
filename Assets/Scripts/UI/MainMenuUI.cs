using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manages the main menu user interface (UI).
/// </summary>
public class MainMenuUI : MonoBehaviour
{
    /// <summary>
    /// Reference to the enemy game object.
    /// </summary>
    [SerializeField] GameObject enemy;

    /// <summary>
    /// Reference to the gun game object
    /// </summary>
    [SerializeField] GameObject gun;

    /// <summary>
    /// Panel for the start menu.
    /// </summary>
    [SerializeField] GameObject startPanel;

    /// <summary>
    /// Panel for the options menu.
    /// </summary>
    [SerializeField] GameObject optionsPanel;

    /// <summary>
    /// Panel for the loading screen.
    /// </summary>
    [SerializeField] GameObject loadingPanel;

    /// <summary>
    /// Slider for the loading progress.
    /// </summary>
    [SerializeField] Slider loadingSlider;

    /// <summary>
    /// Text for the loading progress.
    /// </summary>
    [SerializeField] Text progressText; 

    /// <summary>
    /// Starts the game by loading the game scene.
    /// </summary>
    public void Play()
    {
        Click();

        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Opens the settings menu.
    /// </summary>
    public void OpenSettings()
    {
        Click();

        // Activate the settings panel and hide the main panel
        startPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    /// <summary>
    /// Closes the settings menu.
    /// </summary>
    public void CloseSettings()
    {
        Click();

        // Activate the main panel and deactivate the settings panel
        startPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
    public void Exit()
    {
        Click();

        // Quit the game, either by stopping the editor (if in editor mode) or by quitting the application
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void Start()
    {
        Time.timeScale = 1.0f;

        if (!PlayerPrefs.HasKey("Coins")) FindObjectOfType<SettingsMenu>().ResetProgress();

        startPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    void Update()
    {
        // If any key is pressed, shoot the gun
        if (Input.anyKeyDown)
        {
            gun.GetComponent<GunController>().Shoot();
        }
    }

    /// <summary>
    /// Loads a level asynchronously.
    /// </summary>
    /// <param name="sceneIndex">The index of the scene to load.</param>
    public void LoadLevel(int sceneIndex)
    {
        Click();

        startPanel.SetActive(false);
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingPanel.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            loadingSlider.value = progress;
            progressText.text = $"{progress * 100f}%";

            yield return null;
        }
    }

    private void Click()
    {
        // Play the sound
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }
}
