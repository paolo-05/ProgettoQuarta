using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This script controls the alert for going either back at main menu or reset the game progress.
/// </summary>
public class ModalDialog : MonoBehaviour
{
    /// <summary>
    /// The GameObject representing the modal panel.
    /// </summary>
    [SerializeField] GameObject modalPanelObject;

    // [SerializeField] GameObject blockerPanelObject;

    /// <summary>
    /// The Button for the Yes option.
    /// </summary>
    [SerializeField] Button yesButton;

    /// <summary>
    /// The Button for the No option.
    /// </summary>
    [SerializeField] Button noButton;

    /// <summary>
    /// The Button for the Cancel option.
    /// </summary>
    [SerializeField] Button cancelButton;

    /// <summary>
    /// The current scene.
    /// </summary>
    private int scene;

    private void Start()
    {
        yesButton.onClick.AddListener(OnYesClicked);
        noButton.onClick.AddListener(OnNoClicked);
        cancelButton.onClick.AddListener(OnNoClicked);
    }

    /// <summary>
    /// Shows the dialog by activating the modal panel.
    /// </summary>
    public void ShowDialog()
    {
        modalPanelObject.SetActive(true);
    }

    /// <summary>
    /// Event handler for the Yes button click.
    /// </summary>
    public void OnYesClicked()
    {
        Click();
        modalPanelObject.SetActive(false);
        // blockerPanelObject.SetActive(false);

        scene = SceneManager.GetActiveScene().buildIndex;

        if (scene == 0)
        {
            FindObjectOfType<SettingsMenu>().ResetProgress();
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    /// <summary>
    /// Event handler for the No button click.
    /// </summary>
    public void OnNoClicked()
    {
        Click();

        // Do something when the user clicks the No button.
        // blockerPanelObject.SetActive(false);
        modalPanelObject.SetActive(false);
    }

    /// <summary>
    /// Plays a button click sound.
    /// </summary>
    private void Click()
    {
        // Play the sound
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }
}
