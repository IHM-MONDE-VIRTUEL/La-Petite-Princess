using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Upgrades : MonoBehaviour
{
    [Header("List of upgrades")]
    [Tooltip("The list of upgrades.")]
    public List<GameObject> upgrades;
    private int upgradeIndex = -1;

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
            if (this.upgradeSound != null) AudioSource.PlayClipAtPoint(this.upgradeSound, this.transform.position, 1f);
            this.upgradeIndex++;
            if (this.upgradeIndex > 0) this.upgrades[this.upgradeIndex - 1].SetActive(false);
            this.upgrades[this.upgradeIndex].SetActive(true);
        }
    }
}
