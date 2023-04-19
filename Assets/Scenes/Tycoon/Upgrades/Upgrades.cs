using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using StarterAssets;
using UnityEngine.SceneManagement;

public class Upgrades : MonoBehaviour
{
    [Header("Game Engine")]
    [Tooltip("The game engine to get the money from.")]
    public GameEngine gameEngine;

    [Tooltip("Starting price for upgrades.")]
    [Range(0, 999)]
    public int startingPrice;

    [Tooltip("Starting price unit for upgrades.")]
    [Range(0, 100)]
    public int startingPriceUnit;


    [Header("Upgrades")]
    [Tooltip("The list of upgrade objects.")]
    public List<GameObject> upgrades;
    private int upgradeIndex = -1;

    [Tooltip("The list of upgrade names.")]
    public List<string> upgradeNames;
    private Dictionary<string, int> upgradeLevels = new Dictionary<string, int>();

    [Tooltip("The number of levels for each upgrade.")]
    public int upgradeLevelsCount = 10;


    [Header("Sounds")]
    [Tooltip("The sound to play when an upgrade is bought.")]
    public AudioClip upgradeSound;

    [Tooltip("The player controller to play the sound at.")]
    public ThirdPersonController player;

    void Start()
    {
        foreach (GameObject upgrade in this.upgrades)
        {
            upgrade.SetActive(false);
        }

        foreach (string upgradeName in this.upgradeNames)
        {
            this.upgradeLevels[upgradeName] = 0;
        }
    }

    public void UpgradeLevel()
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

            this.upgradeLevelsCount += 10;
        }
    }

    public bool Buy(string upgradeName, Action update)
    {
        // Check if user has enough money to buy upgrade
        if (!this.gameEngine.spend(this.getPrice(upgradeName))) return false;

        // Upgrade if not maxed
        if (this.upgradeLevels[upgradeName] < this.upgradeLevelsCount) this.upgradeLevels[upgradeName]++;

        // if all upgrades are bought, upgrade the object
        if (this.upgradeLevels.Values.All(level => level == this.upgradeLevelsCount))
        {
            if (this.gameObject.name == "Space Ship") SceneManager.LoadScene("Main Menu");
            if (this.upgradeIndex < this.upgrades.Count - 1)
            {
                // reset upgrade levels
                this.upgradeLevels.Keys.ToList().ForEach(key => this.upgradeLevels[key] = 0);
                this.UpgradeLevel();
            }
        }

        // Refresh HUD
        update();
        return true;
    }

    public void MaxBuy(string upgradeName, Action update)
    {
        int index = this.upgradeIndex;
        while (this.upgradeLevels[upgradeName] < this.upgradeLevelsCount && index == this.upgradeIndex)
        {
            if (!this.Buy(upgradeName, update)) break;
        }
    }

    public Dictionary<string, int> getUpgrades()
    {
        return this.upgradeLevels;
    }

    public Money getPrice(string upgradeName)
    {
        Money target = this.gameEngine.getTarget();
        Money initial = new Money(this.startingPrice, this.startingPriceUnit);
        return initial * (this.upgradeLevels[upgradeName] + 1) * (this.upgradeNames.IndexOf(upgradeName) + 1) * (this.upgradeIndex < 0 ? 1 : Math.Pow(10, this.upgradeIndex + 1) * this.gameEngine.upgradeCostMultiplier);
    }
}
