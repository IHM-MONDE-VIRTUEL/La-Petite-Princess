using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using StarterAssets;
using UnityEditor;


public class Upgrade : MonoBehaviour
{
    public Upgrades upgrades;

    [Tooltip("The upgrade HUD")]
    public UIDocument hud;

    public VisualTreeAsset upgradeUXML;

    [Tooltip("Player")]
    public ThirdPersonController player;

    void Start()
    {
        // Hide upgrade HUD
        this.hud.gameObject.SetActive(false);
    }

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

        var listView = this.hud.rootVisualElement.Q<ListView>("UpgradeList");

        listView.makeItem = () =>
        {
            VisualElement upgradeElement = this.upgradeUXML.Instantiate();

            upgradeElement.Q<Button>("Once").clickable.clicked += () =>
            {
                this.upgrades.Buy(upgradeElement.Q<Label>("Name").text, UpdateUpgradeList);
            };

            upgradeElement.Q<Button>("Max").clickable.clicked += () =>
            {
                this.upgrades.MaxBuy(upgradeElement.Q<Label>("Name").text, UpdateUpgradeList);
            };

            return upgradeElement;
        };

        listView.bindItem = (element, index) =>
        {
            Debug.Log("Binding " + index);
            KeyValuePair<string, int> upgrade = this.upgrades.getUpgrades().ElementAt(index);

            element.Q<Label>("Name").text = upgrade.Key;

            float progress = (float) upgrade.Value / (float) this.upgrades.upgradeLevelsCount * 100;
            element.Q<ProgressBar>("Progress").value = progress;
            element.Q<ProgressBar>("Progress").title = progress + "%";

            element.Q<Label>("Price").text = "0€";
        };

        listView.itemsSource = this.upgrades.upgradeNames;
        listView.selectionType = SelectionType.None;
        listView.Rebuild();
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

    void UpdateUpgradeList()
    {
        var listView = this.hud.rootVisualElement.Q<ListView>("UpgradeList");
        listView.itemsSource = this.upgrades.upgradeNames;
        listView.Rebuild();
    }
}
