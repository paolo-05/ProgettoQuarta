using UnityEngine;
using UnityEngine.SceneManagement; // Need this to manage scene changes

public class GameUI : MonoBehaviour
{
    // Function to start the game again
    public void PlayAgain()
    {
        // Load scene with index 1 (the game scene)
        SceneManager.LoadScene(1);
    }

    // Function to go back to the main menu
    public void MainMenu()
    {
        // Load scene with index 0 (the main menu scene)
        SceneManager.LoadScene(0);
    }
}
