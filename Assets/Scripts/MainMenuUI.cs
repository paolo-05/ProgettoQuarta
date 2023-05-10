using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{

    [SerializeField] GameObject enemy;
    [SerializeField] GameObject gun;

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {
        // to - do
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            gun.GetComponent<GunController>().Shoot();
        }
    }
}
