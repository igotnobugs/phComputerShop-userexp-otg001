using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

/* WIP
 * 
 * Make Phases scaleable.
 */

public enum GamePhase { StartTransistion, NightToDawnTransistion, SettingUp,
                        OpenStore, StartClosing, ClosingSequence, AwaitNextDay}

public class GameManager : Singleton<GameManager> 
{
    public static GamePhase Phase { get; private set; } = GamePhase.StartTransistion;

    [Header("Setting Up")]
    [SerializeField] public TransistionUI transistion;
    [SerializeField] public OutsideLight sun;
    [SerializeField] public TextMeshProUGUI phaseInitial;
    [SerializeField] public TransistionUI openShopButton;
    [SerializeField] public RadialTimer radialTimer;
    [SerializeField] private StaffPanelManager staffPanel;
    [SerializeField] private MainUISequence uiSequence;

    [Header("Game Related")]
    [SerializeField] public Staff[] staffs;
    [SerializeField] public float shopOpenTime = 10.0f;

    public StoreData store = new StoreData();

    private void Awake() {
        staffPanel = GetComponent<StaffPanelManager>();
        uiSequence = GetComponent<MainUISequence>();
    }

    private void Start() {
        Phase = GamePhase.StartTransistion;

        // Once the transistion is done, set up the game
        sun.EarlyMorning(3.0f);

        transistion.Hide().setDelay(0.2f).setOnComplete( () => {   
            SetUpGame();
        });
    }

    // Only called at the start of the game
    private void SetUpGame() {      
        uiSequence.StartSequence(() => StartEarlyMorning());
    }

    // The first day after transition
    private void StartEarlyMorning() {
        Phase = GamePhase.NightToDawnTransistion;

        for (int i = 0; i < staffs.Length; i++) {
            staffPanel.CreateStaffButton(staffs[i]);
        }

        new Timer().StartTimer(gameObject, 1.0f).setOnComplete(() => SetUpPhase());

        phaseInitial.text = "<color=red>C</color>";

    }

    // After the night after
    private void StartEarlyMorningLoop() {
        Phase = GamePhase.NightToDawnTransistion;
        
        sun.EarlyMorning(2.0f);

        new Timer().StartTimer(gameObject, 2.0f);
        uiSequence.StartSequence(() => SetUpPhase());

        phaseInitial.text = "<color=red>C</color>";
    }

    // Setting Up Phase, Can only advance when the button is clicked
    public void SetUpPhase() {
        Phase = GamePhase.SettingUp;

        openShopButton.Show();
    }

    // When store is open, called by the button
    public void OpenStore() {
        Phase = GamePhase.OpenStore;

        phaseInitial.text = "<color=green>O</color>";
        openShopButton.Hide();
        sun.WorkingDays(shopOpenTime);

        radialTimer.StartTimerRadial(shopOpenTime, () => StartClosing());
    }

    // Store is closing
    private void StartClosing() {
        Phase = GamePhase.StartClosing;

        // Check for events here maybe?

        phaseInitial.text = "<color=red>C</color>";
        StartCoroutine(ClosingSequence());
    }

    // Do a sequence of closing work here
    private IEnumerator ClosingSequence() {
        Phase = GamePhase.ClosingSequence;

        uiSequence.StartSequence(null, false);

        // Mostly just money deductions and stuff

        yield return new WaitForSeconds(4.0f);

        SetUpNextDay();
        yield return null;
    }

    // Get ready for the next day
    private void SetUpNextDay() {
        Phase = GamePhase.AwaitNextDay;

        sun.GoToNextDay();

        
        StartEarlyMorningLoop();
    }
}
