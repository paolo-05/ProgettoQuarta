
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    // Reference to the enemy and gun game objects
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject gun;

    // Panels for responsive UI
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject optionsPanel;
    [SerializeField] GameObject loadingPanel;
    [SerializeField] Slider loadingSlider;
    [SerializeField] Text progressText;

    // Start the game by loading the game scene
    public void Play()
    {
        Click();

        SceneManager.LoadScene(1);
    }

    // Open the settings menu
    public void OpenSettings()
    {
        Click();

        // activate the settings panel and hide the main panel
        startPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        Click();

        // activate the main panel and deactivate the settings panel
        startPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    // Quit the game
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

    // Update is called once per frame
    void Update()
    {
        // If any key is pressed, shoot the gun
        if (Input.anyKeyDown)
        {
            gun.GetComponent<GunController>().Shoot();
        }
    }

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
