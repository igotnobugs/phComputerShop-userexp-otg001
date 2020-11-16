using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    public int currency = 10000;
    public Text currencyText;

    void Update()
    {
        currencyText.text = "Currency : ₱" + currency;
    }
}
