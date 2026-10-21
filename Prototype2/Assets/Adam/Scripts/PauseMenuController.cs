using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [Header("UI Reference")]
    public GameObject pauseMenuUI;

    public static bool isPaused = false;

    void Update()
    {
        // Toggle Pause Menu with 'P' key
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resumes game time
        isPaused = false;

        // Unlock cursor for gameplay if needed
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Freezes game physics and animations
        isPaused = true;

        // Enable cursor for UI navigation
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OpenSettings()
    {
        // Optional: Open Settings sub-panel or scene
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Reset time back to normal before leaving
        SceneManager.LoadScene("MainMenu");
    }
}