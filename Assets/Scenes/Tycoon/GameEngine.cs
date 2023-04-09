using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameEngine : MonoBehaviour
{
    private Money bank = new Money();
    private Money wallet = new Money();
    private Money rate = new Money(1);

    public string bankAmount;
    public string walletAmount;
    public string rateAmount;

    public TMP_Text bankText;
    public TMP_Text walletText;
    public TMP_Text rateText;


    // Start is called before the first frame update
    void Start()
    {
        if (this.bankAmount != "") this.bank = Money.Parse(this.bankText.text);
        if (this.walletAmount != "") this.wallet = Money.Parse(this.walletText.text);
        if (this.rateAmount != "") this.rate = Money.Parse(this.rateText.text);
    }

    // Update is called once per frame
    void Update()
    {
        this.bank += this.rate * Time.deltaTime;
        
        if (this.bankText != null) this.bankText.text = this.bank.ToString();
        if (this.walletText != null) this.walletText.text = this.wallet.ToString();
        if (this.rateText != null) this.rateText.text = this.rate.ToString();
    }
}
