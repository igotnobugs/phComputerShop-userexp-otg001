using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManagementTab : MonoBehaviour 
{
    public Image pricePanel;
    public TextMeshProUGUI priceAmt;
    public Slider price;

    public Image wagesPanel;
    public TextMeshProUGUI wagesAmt;
    public Slider wages;

    public Toggle overTimeToggle;

    public Button staffButton;
    public Button storeButton;

    public Color enabledColor;
    public Color disabledColor;

    private void Start() {
        price.value = GameManager.store.price;
        wages.value = wagesFloatToInt(GameManager.store.wageMult);

        DisallowInteraction();
    }

    private void Update() {
        priceAmt.text = price.value.ToString();
        wagesAmt.text = wagesIntToFloat(wages.value).ToString() + "x";
    }

    private int wagesFloatToInt(float value) {
        float multValue = value;
        float taken = multValue * 4;
        return (int)taken;
    }

    private float wagesIntToFloat(float value) {
        return value / 4.0f;
    }

    public void AllowInteraction() {
        price.interactable = true;
        wages.interactable = true;
        overTimeToggle.interactable = true;
        staffButton.interactable = true;
        storeButton.interactable = true;
        pricePanel.color = enabledColor;
        wagesPanel.color = enabledColor;
    }

    public void DisallowInteraction() {
        price.interactable = false;
        wages.interactable = false;
        overTimeToggle.interactable = false;
        staffButton.interactable = false;
        storeButton.interactable = false;
        pricePanel.color = disabledColor;
        wagesPanel.color = disabledColor;
        
    }

    public void SetValues() {
        GameManager.store.price = (int)price.value;
        GameManager.store.wageMult = wagesIntToFloat(wages.value);
    }
}
