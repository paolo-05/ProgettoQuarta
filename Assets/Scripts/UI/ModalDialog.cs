using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModalDialog : MonoBehaviour
{
    [SerializeField] GameObject modalPanelObject;
    // [SerializeField] GameObject blockerPanelObject;
    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;
    [SerializeField] Button cancelButton;

    private int scene;

    private void Start()
    {
        // modalPanelObject.SetActive(false);
        // blockerPanelObject.SetActive(false);
        yesButton.onClick.AddListener(OnYesClicked);
        noButton.onClick.AddListener(OnNoClicked);
        cancelButton.onClick.AddListener(OnNoClicked);
    }

    public void ShowDialog()
    {
        modalPanelObject.SetActive(true);
    }

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

    public void OnNoClicked()
    {
        Click();

        // Do something when the user clicks the No button.
        // blockerPanelObject.SetActive(false);
        modalPanelObject.SetActive(false);

    }

    private void Click()
    {
        // Play the sound
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }
}
