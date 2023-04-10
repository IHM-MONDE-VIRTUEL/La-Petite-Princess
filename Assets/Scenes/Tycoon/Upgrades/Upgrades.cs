using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Upgrades : MonoBehaviour
{
    [Header("List of upgrades")]
    [Tooltip("The list of upgrades.")]
    public List<GameObject> upgrades;
    private int upgradeIndex = -1;

    [Tooltip("Player")]
    public ThirdPersonController player;

    [Header("Sounds")]
    [Tooltip("The sound to play when an upgrade is bought.")]
    public AudioClip upgradeSound;

    void Start()
    {
        foreach (GameObject upgrade in this.upgrades)
        {
            upgrade.SetActive(false);
        }
    }

    public void buy()
    {
        if (this.upgradeIndex < this.upgrades.Count - 1)
        {
            // Check if the sound is playing and stop it
            if (this.upgradeSound != null)
            {
                // Stop any upgrade sound that is playing
                foreach (AudioSource audioSource in FindObjectsOfType<AudioSource>())
                {
                    if (audioSource.clip == this.upgradeSound) audioSource.Stop();
                }
                AudioSource.PlayClipAtPoint(this.upgradeSound, this.player.transform.position);
            }

            this.upgradeIndex++;
            if (this.upgradeIndex > 0) this.upgrades[this.upgradeIndex - 1].SetActive(false);
            this.upgrades[this.upgradeIndex].SetActive(true);
        }
    }
}
