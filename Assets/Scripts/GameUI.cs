using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; // Need this to manage scene changes

public class GameUI : MonoBehaviour
{
    // Game objects in the scene
    [SerializeField] GameObject coinsGameObject;
    [SerializeField] GameObject startGame;
    [SerializeField] GameObject purchasePanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] ModalDialog alert;

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
        alert.ShowDialog();
    }

    public void OpenSettings()
    {
        Click();

        // hide the other elements in UI
        coinsGameObject.SetActive(false);
        startGame.SetActive(false);
        purchasePanel.SetActive(false);

        // open the settings by activating the panel
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        Click();

        // since the settings can only be opened if the game isn't started, the element that we must re-activate are:
        coinsGameObject.SetActive(true);
        startGame.SetActive(true);
        purchasePanel.SetActive(true);

        // open the settings by activating the panel
        settingsPanel.SetActive(false);
    }

    public void Pause()
    {
        Click();

        // implement the pause
    }

    private void Click()
    {
        // Play the sound
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }
}
