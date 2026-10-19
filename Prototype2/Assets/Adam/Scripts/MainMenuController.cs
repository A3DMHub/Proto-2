using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Call this when clicking "Start"
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Call this when clicking "Settings"
    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    // Call this when clicking "Back" inside the Settings scene
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Call this when clicking "Quit"
    public void QuitGame()
    {
        Debug.Log("Quit Game pressed!");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}