using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class GameEngine : MonoBehaviour
{
    public static int MAX_VISUAL_UPDATE_FRAMES = 10;
    private int frameCount = 0;

    private Money bank;
    private Money wallet;
    private Money rate;

    [Header("Bank Amount")]
    [Tooltip("The amount of money in the bank.")]
    [Range(0, 999)]
    public int bankAmount;

    [Tooltip("The unit of the amount of money in the bank.")]
    [Range(0, 100)]
    public int bankUnit;


    [Header("Wallet Amount")]
    [Tooltip("The amount of money in the wallet.")]
    [Range(0, 999)]
    public int walletAmount;

    [Tooltip("The unit of the amount of money in the wallet.")]
    [Range(0, 100)]
    public int walletUnit;


    [Header("Rate Amount")]
    [Tooltip("The amount of money per second.")]
    [Range(1, 999)]
    public int rateAmount;

    [Tooltip("The unit of the amount of money per second.")]
    [Range(0, 100)]
    public int rateUnit;

    public TMP_Text bankText;

    public UIDocument hud;
    private Label walletTextElement;
    private Label rateTextElement;


    // Start is called before the first frame update
    void Start()
    {
        this.walletTextElement = this.hud.rootVisualElement.Q<Label>("Amount");
        this.rateTextElement = this.hud.rootVisualElement.Q<Label>("Rate");

        this.bank = new Money(this.bankAmount, this.bankUnit);
        this.wallet = new Money(this.walletAmount, this.walletUnit);
        this.rate = new Money(this.rateAmount, this.rateUnit);

        this.updateBankUI();
        this.updateWalletUI();
        this.updateRateUI();
    }

    // Update is called once per frame
    void Update()
    {
        this.bank += this.rate * Time.deltaTime;

        // Update UI only every 10 frames
        if (this.frameCount++ < GameEngine.MAX_VISUAL_UPDATE_FRAMES) return;
        this.frameCount = 0;

        this.updateBankUI();
    }

    public void collectBank()
    {
        this.wallet += this.bank;
        this.bank = new Money();

        this.updateWalletUI();
    }

    public void updateBankUI()
    {
        if (this.bankText != null) this.bankText.text = this.bank.ToString();
    }

    public void updateWalletUI()
    {
        if (this.walletTextElement != null) this.walletTextElement.text = this.wallet.ToString();
    }

    public void updateRateUI()
    {
        if (this.rateTextElement != null) this.rateTextElement.text = this.rate.ToString() + "/s";
    }
}
