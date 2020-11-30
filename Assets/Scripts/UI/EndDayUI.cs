using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* Should be called the endDayPhase and not UI
 * 
 */

public class EndDayUI : MonoBehaviour
{
    public float sequenceSpeed = 0.5f;
    public Button confirm;
    public TransistionUI transistion;

    public TextMeshProUGUI earnings;
    public TextMeshProUGUI wages;
    public TextMeshProUGUI profits;
    public TextMeshProUGUI total;

    public Color positiveNetProfit;
    public Color negativeNetProfit;

    public void Init(Action buttonOnClick) {
        confirm.onClick.AddListener(() => buttonOnClick?.Invoke());

        confirm.gameObject.SetActive(false);
    }

    // Enumerate
    public void Calculate() {
        StartCoroutine(StartCalculating());
    }

    private IEnumerator StartCalculating() {
        earnings.text = GameManager.store.earnings.ToString();
        yield return new WaitForSeconds(sequenceSpeed);

        //wage test
        int wage = -200;
        wages.text = wage.ToString();
        yield return new WaitForSeconds(sequenceSpeed);

        int profit = GameManager.store.earnings + wage;
        profits.text = "";
        if (profit > 0) {
            profits.color = positiveNetProfit;
            profits.text = "+";
        }
        else if (profit == 0) {
            profits.color = positiveNetProfit;
        }
        else {
            profits.color = negativeNetProfit;
        }
        profits.text += profit.ToString();
        yield return new WaitForSeconds(sequenceSpeed);

    
        GameManager.store.AddMoneyNoEarnings(profit);
        total.text = GameManager.store.money.ToString();

        confirm.gameObject.SetActive(true);

        yield break;
    }

}
