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
        earnings.gameObject.SetActive(false);
        wages.gameObject.SetActive(false);
        profits.gameObject.SetActive(false);
        total.gameObject.SetActive(false);
        StartCoroutine(StartCalculating());
    }

    private IEnumerator StartCalculating() {
        //Earnings
        earnings.gameObject.SetActive(true);
        earnings.text = GameManager.store.Earnings.ToString();
        yield return new WaitForSeconds(sequenceSpeed);

        //Wages
        wages.gameObject.SetActive(true);
        int wage = GameManager.store.GetTotalWages();
        GameManager.store.DeductMoney(Mathf.Abs(wage));
        wages.text = wage.ToString();
        yield return new WaitForSeconds(sequenceSpeed);

        //Profit
        profits.gameObject.SetActive(true);
        int profit = GameManager.store.GetProfit();
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

        //Total
        total.gameObject.SetActive(true);
        GameManager.store.ModifyMoney(profit);
        total.text = GameManager.store.money.ToString();

        confirm.gameObject.SetActive(true);

        yield break;
    }

}
