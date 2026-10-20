using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // --- MAIN MENU NAVIGATION ---
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // --- PAUSE MENU NAVIGATION ---
    public void OpenSettingsPauseMenu()
    {
        SceneManager.LoadScene("SettingsPauseMenu");
    }

    public void BackToPauseMenu()
    {
        SceneManager.LoadScene("PauseMenu");
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