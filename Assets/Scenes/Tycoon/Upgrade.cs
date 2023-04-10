using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using StarterAssets;


public class Upgrade : MonoBehaviour
{
    [Tooltip("The game engine")]
    public GameEngine gameEngine;

    [Tooltip("The upgrade HUD")]
    public UIDocument hud;

    [Tooltip("Player")]
    public ThirdPersonController player;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;

        // Show upgrade HUD
        this.hud.gameObject.SetActive(true);

        // Make mouse visible and disable mouse lock
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;

        // Disable player movement and camera movement
        this.player.LockCameraPosition = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player") return;

        // Hide upgrade HUD
        this.hud.gameObject.SetActive(false);

        // Make mouse invisible and enable mouse lock and enable player movement and camera movement
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

        // Enable player movement and camera movement
        this.player.LockCameraPosition = false;
    }
}
