using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // --- MAIN MENU NAVIGATION ---
    public void PlayGame()
    {
        // Loads the level created in Assets/Freyja/Scene/
        SceneManager.LoadScene("Tutorial level");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void BackToMainMenu()
    {
        // Unpause time if returning to main menu while paused
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    // --- QUIT ---
    public void QuitGame()
    {
        Debug.Log("Quit Game pressed!");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}