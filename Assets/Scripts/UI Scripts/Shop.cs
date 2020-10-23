using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public int currency = 10000;
    public Text currencyText;
    public Text inventory;

    public void addItem(string item)
    {
        currencyText.ToString();
        inventory.text += "\n" + item;
    }
}
