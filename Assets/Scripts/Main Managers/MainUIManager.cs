using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class MainUIManager : Singleton<MainUIManager> 
{
    public BaseUI management;
    public BaseUI ledger;
    public BaseUI shop;
    public BaseUI staffPanel;
    public BaseUI clipboard;

    public TransistionUI[] transistionInterfaces;

    [Header("Top Right Panel")]
    [SerializeField] private TextMeshProUGUI availableMoney = null;
    [SerializeField] private TextMeshProUGUI moneyGoal = null;
    [SerializeField] private TextMeshProUGUI day = null;
    [SerializeField] private TextMeshProUGUI weekMonth = null;

    private Action onCompleteFunction;
    private bool isUIShown;
    private TransistionUI shopTransisition = null;

    public void StartSequence(Action onComplete, bool showUI = true) {
        onCompleteFunction = onComplete;
        if (showUI) {
            StartCoroutine(StartShowSequence());
        }
        else {
            StartCoroutine(StartHideSequence());
        }
        shopTransisition = shop.GetComponent<TransistionUI>();
    }

    public void Update() {
        availableMoney.text = GameManager.store.money.ToString();
        moneyGoal.text = GameManager.store.moneyNeeded.ToString();
        day.text = GameManager.store.GetCurrentDayShortString();
        weekMonth.text = GameManager.store.GetWeekMonth();
    }

    private IEnumerator StartShowSequence(float speed = 0.5f) {
        if (isUIShown) yield break;
        isUIShown = true;

        for (int i = 0; i < transistionInterfaces.Length; i++) {
            transistionInterfaces[i].Show();
            yield return new WaitForSeconds(speed);
        }

        yield return new WaitForSeconds(speed);
        
        onCompleteFunction?.Invoke();
        yield break;
    }

    private IEnumerator StartHideSequence(float speed = 0.2f) {
        if (!isUIShown) yield break;
        isUIShown = false;

        for (int i = transistionInterfaces.Length - 1; i >= 0; i--) {
            transistionInterfaces[i].Hide();
            yield return new WaitForSeconds(speed);
        }

        yield return new WaitForSeconds(speed);

        onCompleteFunction?.Invoke();
        yield break;
    }

    // During set up
    public void StartSetUpPhase() {
        shopTransisition.Show();
    }

    // During open phase
    public void StartOpenPhase() {
        shopTransisition.Hide();
    }
}
