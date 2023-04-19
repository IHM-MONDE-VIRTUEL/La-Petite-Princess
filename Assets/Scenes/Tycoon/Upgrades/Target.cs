using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("Game Engine")]
    [Tooltip("The game engine to get the target money from.")]
    public GameEngine gameEngine;

    [Tooltip("To change the target money.")]
    public Upgrades upgrades;

    // Start is called before the first frame update
    void Start()
    {
        Money upgradePrice = new Money(this.gameEngine.getTarget().GetValue() / this.upgrades.upgradeLevelsCount, this.gameEngine.getTarget().GetUnit());

        this.upgrades.startingPrice = (int)upgradePrice.GetValue();
        this.upgrades.startingPriceUnit = upgradePrice.GetUnit();
    }
}
