using UnityEngine;
using TMPro; // Delete or comment out this line if you are using standard Unity UI Text instead of TextMeshPro

public class TutorialPopup : MonoBehaviour
{
    [Header("UI Reference")]
    public GameObject tutorialTextObject; // Drag your text UI object here

    void Start()
    {
        // Ensure the text is hidden when the game starts
        if (tutorialTextObject != null)
        {
            tutorialTextObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the zone (make sure your player has the "Player" tag, or remove this check)
        if (other.CompareTag("Player"))
        {
            if (tutorialTextObject != null)
            {
                tutorialTextObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player left the zone
        if (other.CompareTag("Player"))
        {
            if (tutorialTextObject != null)
            {
                tutorialTextObject.SetActive(false);
            }
        }
    }
}