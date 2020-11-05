using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* Handles the phases.
 * 
 * RUSHED!
 * WIP
 */

public enum GamePhase { StartTransistion, NightToDawnTransistion, SettingUp,
                        OpenStore, StartClosing, ClosingSequence, AwaitNextDay}

public class GameManager : Singleton<GameManager> 
{
    public static GamePhase Phase { get; private set; } = GamePhase.StartTransistion;

    [Header("Setting Up")]
    [SerializeField] public UITween transistion;
    [SerializeField] public OutsideLight sun;
    [SerializeField] public TextMeshProUGUI phaseInitial;
    [SerializeField] public UITween openShopButton;
    [SerializeField] private TimerControl timer;
    [SerializeField] private StaffPanelManager staffPanel;

    [Header("Game Related")]
    [SerializeField] public Staff[] staffs;
    [SerializeField] public float shopOpenTime = 10.0f;

    private void Awake() {
        staffPanel = GetComponent<StaffPanelManager>();
        timer = GetComponent<TimerControl>();
    }

    private void Start() {
        Phase = GamePhase.StartTransistion;

        // Once the transistion is done, set up the game
        void endFunction() { SetUpGame(); }
        transistion.GoToRelativePosition(endFunction);
    }

    // Only called at the start of the game
    private void SetUpGame() {
        // Create the staff buttons
        for (int i = 0; i < staffs.Length; i++) {
            staffPanel.CreateStaffButton(staffs[i]);
        }

        StartEarlyMorning();
    }

    // After the night
    private void StartEarlyMorning() {
        Phase = GamePhase.NightToDawnTransistion;
     
        sun.EarlyMorning(2.0f);

        void endFunction() { SetUpPhase(); }
        timer.StartTimer(2.0f, endFunction);

        phaseInitial.text = "<color=red>C</color>";
    }

    // Setting Up Phase, Can only advance when the button is clicked
    public void SetUpPhase() {
        Phase = GamePhase.SettingUp;

        openShopButton.GoToRelativePosition();
    }

    // When store is open, called by the button
    public void OpenStore() {
        Phase = GamePhase.OpenStore;

        phaseInitial.text = "<color=green>O</color>";
        openShopButton.GoToDefaultPosition();
        sun.WorkingDays(shopOpenTime);

        void endFunction() { StartClosing(); }    
        timer.StartTimerRadial(shopOpenTime, endFunction);
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

        // Mostly just money deductions and stuff

        yield return new WaitForSeconds(2.0f);

        SetUpNextDay();
        yield return null;
    }

    // Get ready for the next day
    private void SetUpNextDay() {
        Phase = GamePhase.AwaitNextDay;

        sun.GoToNextDay();

        StartEarlyMorning();
    }
}
