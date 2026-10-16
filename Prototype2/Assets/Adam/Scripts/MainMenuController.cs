using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Call this when clicking "Start"
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Call this when clicking "Quit"
    public void QuitGame()
    {
        Debug.Log("Quit Game pressed!");
        Application.Quit();
    }
}