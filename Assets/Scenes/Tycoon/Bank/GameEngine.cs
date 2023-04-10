using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class GameEngine : MonoBehaviour
{
    public static int MAX_VISUAL_UPDATE_FRAMES = 10;
    private int frameCount = 0;

    private Money target;

    private Money bank;
    private Money wallet;
    private Money rate;

    [Header("Target Amount")]
    [Tooltip("The amount of money to reach.")]
    [Range(0, 999)]
    public int targetAmount;

    [Tooltip("The unit of the amount of money to reach.")]
    [Range(0, 100)]
    public int targetUnit;

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
    private ProgressBar progressBar;


    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

        this.walletTextElement = this.hud.rootVisualElement.Q<Label>("Amount");
        this.rateTextElement = this.hud.rootVisualElement.Q<Label>("Rate");
        this.progressBar = this.hud.rootVisualElement.Q<ProgressBar>("Target");

        this.target = new Money(this.targetAmount, this.targetUnit);
        this.bank = new Money(this.bankAmount, this.bankUnit);
        this.wallet = new Money(this.walletAmount, this.walletUnit);
        this.rate = new Money(this.rateAmount, this.rateUnit);

        if (this.target == new Money()) this.progressBar.style.display = DisplayStyle.None;
        else
        {
            this.progressBar.title = this.wallet.ToString() + " / " + this.target.ToString();
            this.progressBar.value = 0;
        }

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
        this.bankText.text = this.bank.ToString();
    }

    public void updateWalletUI()
    {
        this.walletTextElement.text = this.wallet.ToString();
        if (this.target != new Money() && this.wallet > new Money())
        {
            this.progressBar.title = this.wallet.ToString() + " / " + this.target.ToString();

            float value = 0;
            if (this.wallet >= this.target) value = 1;
            else
            {
                float percentage = (float)(this.wallet / this.target);
                if (percentage == float.PositiveInfinity || percentage < 0) value = 0;
            }
            Debug.Log(value * 100 + "%" + " " + this.wallet + " " + this.target);
            this.progressBar.value = value * 100;
        }
    }

    public void updateRateUI()
    {
        this.rateTextElement.text = this.rate.ToString() + "/s";
    }
}
