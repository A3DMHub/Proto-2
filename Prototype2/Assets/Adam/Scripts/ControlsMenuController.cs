using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ControlsMenuController : MonoBehaviour
{
    [Header("UI References")]
    public GameObject controlsPanelUI;
    public GameObject promptTextUI;

    private bool isControlsOpen = false;

    private void Start()
    {
        
        if (controlsPanelUI != null)
        {
            isControlsOpen = false;
            controlsPanelUI.SetActive(false);
        }

        if (promptTextUI != null)
        {
            promptTextUI.SetActive(true);
        }
    }

    private void Update()
    {
        // Check if TAB was pressed
        if (Keyboard.current != null && Keyboard.current.tabKey.wasPressedThisFrame)
        {
            ToggleControls();
        }
    }

    public void ToggleControls()
    {
        isControlsOpen = !isControlsOpen;

        if (controlsPanelUI != null)
        {
            controlsPanelUI.SetActive(isControlsOpen);
        }
    }
}