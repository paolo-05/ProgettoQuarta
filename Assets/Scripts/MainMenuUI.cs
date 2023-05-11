
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    // Reference to the enemy and gun game objects
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject gun;

    // Start the game by loading the game scene
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    // Open the settings menu (to be implemented)
    public void OpenSettings()
    {
        // TODO: Implement this method
    }

    // Quit the game
    public void Exit()
    {
        // Quit the game, either by stopping the editor (if in editor mode) or by quitting the application
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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

}
