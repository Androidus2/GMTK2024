using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateExplanation : MonoBehaviour
{
    [SerializeField] 
    private GameObject panel;
    [SerializeField]
    private bool wasTriggered = false;
    [SerializeField]
    private PlayerLook playerMovementScript; // Reference to the player's movement script


    void OnTriggerEnter(Collider other)
    {
        if (panel != null && other.gameObject.layer == LayerMask.NameToLayer("Player") && !wasTriggered)
        {
            panel.SetActive(true); // Enable the panel when the trigger event occurs
            wasTriggered = true;
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            Cursor.visible = true; // Make the cursor visibl
            if (playerMovementScript != null)
            {
                playerMovementScript.enabled = false; // Disable the player's movement script
            }
        }
    }

    public void ExitPanel() {
        if (panel != null && panel.activeSelf && wasTriggered)
        {
            panel.SetActive(false); // Disable the panel when the trigger event occurs
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
            Cursor.visible = false; // Hide the cursor
            if (playerMovementScript != null)
            {
                playerMovementScript.enabled = true; // Re-enable the player's movement script
            }
        }
    }
}
