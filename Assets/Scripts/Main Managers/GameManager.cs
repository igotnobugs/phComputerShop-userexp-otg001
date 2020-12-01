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
    [SerializeField] private MainUIManager mainUIManager;
    [SerializeField] private DialogueTrigger introDialogue = null;
    [SerializeField] private PopUpManager popUpManager = null;
    [SerializeField] private EndDayUI endDayPanel = null;

    [Header("Game Related")]
    [SerializeField] public Staff[] staffs;
    [SerializeField] public float shopOpenTime = 20.0f;

    public StoreData storeData;
    public static StoreData store;

    [Header("Testing")]
    public bool skipTutorial = true;
    public Transform door;
    public Customer testCustomer;
    public List<Customer> customers;


    private void Awake() {
        store = storeData;
        staffPanel = GetComponent<StaffPanelManager>();
        mainUIManager = GetComponent<MainUIManager>();
    }

    private void Start() {
        Phase = GamePhase.StartTransistion;

        sun.EarlyMorning(3.0f);

        // Once the transistion is done, set up the game      
        // If game is new do this
        transistion.Hide(() => {
            if (skipTutorial) {
                mainUIManager.StartSequence(() => {
                    StartEarlyMorning();
                });
                
            } else {
                StartCoroutine(NewGameSequence());
            }

        }).setDelay(0.5f);
    }

    // Includes tutorials
    private IEnumerator NewGameSequence() {
        //Do the introduction scene
        bool isIntroDone = false;
        introDialogue.TriggerDialogue(() => isIntroDone = true);
        yield return new WaitUntil(() => isIntroDone);

        bool isPopUpDone = false;
        Vector2 uiPosition = mainUIManager.management.transform.position;
        Vector2 pivotPosition = new Vector2(0, 0);
        PopUp tutorialPopUp = null;

        //Camera Control Popup
        isPopUpDone = false;
        uiPosition = new Vector2(250, 150);
        pivotPosition = new Vector2(0.5f, 0.5f);
        tutorialPopUp = popUpManager.CreatePopUp(pivotPosition, uiPosition);
        tutorialPopUp.Init("Camera Controls", "Drag the camera around by holding the Middle Mouse button." +
            "\nPress Space to return to the center." +
            "\nClose the Pop Up to advance.");
        tutorialPopUp.SetListener(mainUIManager.clipboard.GetComponent<ActiveUI>());
        tutorialPopUp.SetOnComplete(() => isPopUpDone = true);
        yield return new WaitUntil(() => isPopUpDone);

        // Sequence
        bool isSequenceDone = false;
        mainUIManager.StartSequence(() => isSequenceDone = true);
        yield return new WaitUntil(() => isSequenceDone);

        //Management Popup
        isPopUpDone = false;
        uiPosition = mainUIManager.management.transform.position;
        pivotPosition = new Vector2(0, 0);
        tutorialPopUp = popUpManager.CreatePopUp(pivotPosition, uiPosition);
        tutorialPopUp.Init("Management", "This allows you to change prices, staff and the shop.");
        tutorialPopUp.SetListener(mainUIManager.management.GetComponent<ActiveUI>());
        tutorialPopUp.SetOnComplete(() => isPopUpDone = true);      
        yield return new WaitUntil(() => isPopUpDone);

        //Ledger Popup
        isPopUpDone = false;
        uiPosition = mainUIManager.ledger.transform.position + new Vector3(50, 0, 0);
        pivotPosition = new Vector2(0, 0);
        tutorialPopUp = popUpManager.CreatePopUp(pivotPosition, uiPosition);
        tutorialPopUp.Init("Ledger", "Contains all the previous transactions.");
        tutorialPopUp.SetListener(mainUIManager.ledger.GetComponent<ActiveUI>());
        tutorialPopUp.SetOnComplete(() => isPopUpDone = true);
        yield return new WaitUntil(() => isPopUpDone);

        //Shop Popup
        isPopUpDone = false;
        uiPosition = mainUIManager.shop.transform.position + new Vector3(100, 0, 0);
        pivotPosition = new Vector2(0, 0);
        tutorialPopUp = popUpManager.CreatePopUp(pivotPosition, uiPosition);
        tutorialPopUp.Init("Store", "Lets you buy furnitures and upgrades.\nIt disappears when you start opening and will not be available until the next day.");
        tutorialPopUp.SetListener(mainUIManager.shop.GetComponent<ActiveUI>());
        tutorialPopUp.SetOnComplete(() => isPopUpDone = true);
        yield return new WaitUntil(() => isPopUpDone);

        //Clipboard Popup
        isPopUpDone = false;
        uiPosition = mainUIManager.clipboard.transform.position;
        pivotPosition = new Vector2(1, 0);
        tutorialPopUp = popUpManager.CreatePopUp(pivotPosition, uiPosition);
        tutorialPopUp.Init("Clipboard", "Shows the attributes of a selected staff.");
        tutorialPopUp.SetListener(mainUIManager.clipboard.GetComponent<ActiveUI>());
        tutorialPopUp.SetOnComplete(() => isPopUpDone = true);
        yield return new WaitUntil(() => isPopUpDone);

        //Set up the game
        StartEarlyMorning();        
        yield break;
    }

    // The first day after transition
    private void StartEarlyMorning() {
        Phase = GamePhase.NightToDawnTransistion;    

        for (int i = 0; i < staffs.Length; i++) {
            staffPanel.CreateStaffButton(staffs[i]);
            staffs[i].EnableSelection();
        }

        new Timer().StartTimer(gameObject, 1.0f).setOnComplete(() => SetUpPhase());

        phaseInitial.text = "<color=red>C</color>";

    }

    // next day LOOP
    private void StartEarlyMorningLoop() {
        Phase = GamePhase.NightToDawnTransistion;
        store.AdvanceDay();
        sun.EarlyMorning(2.0f);

        new Timer().StartTimer(gameObject, 2.0f);

        mainUIManager.StartSequence(() => SetUpPhase());

        phaseInitial.text = "<color=red>C</color>";
    }

    // Setting Up Phase, Can only advance when the button is clicked
    public void SetUpPhase() {
        Phase = GamePhase.SettingUp;
      
        openShopButton.Show();

        //Start Popup Popup
        Vector2 uiPosition = mainUIManager.clipboard.transform.position + new Vector3(0, 20);
        Vector2 pivotPosition = new Vector2(1, 0);
        PopUp tutorialPopUp = popUpManager.CreatePopUp(pivotPosition, uiPosition);
        tutorialPopUp.Init("Start", "Before you open the shop, make sure the counter is manned by clicking one of your staffs and Right-Click the Counter then choose Interact.");
        tutorialPopUp.SetListener(openShopButton.GetComponent<TransistionUI>(), true);
        tutorialPopUp.SetOnComplete(() => Destroy(tutorialPopUp));
    }

    // When store is open, called by the button
    public void OpenStore() {
        Phase = GamePhase.OpenStore;

        //Spawn at the door
        Customer newCustomer = Instantiate(testCustomer, door.position, testCustomer.transform.rotation);
        newCustomer.door = door;
        newCustomer.costumerList = customers;

        customers.Add(newCustomer);
        

        phaseInitial.text = "<color=green>O</color>";
        mainUIManager.StartOpenPhase();
        openShopButton.Hide();
        sun.WorkingDays(shopOpenTime);

        radialTimer.StartTimerRadial(shopOpenTime, () => StartClosing());
    }

    // Store is closing
    private void StartClosing() {
        Phase = GamePhase.StartClosing;

        // Make customers leave forcefully if present
        if (customers.Count > 0) {
            foreach (Customer c in customers) {
                c.LeaveTheStore();
            }         
        }

        // Check for events here maybe?

        phaseInitial.text = "<color=red>C</color>";
        StartCoroutine(ClosingSequence());
    }

    // Do a sequence of closing work here
    private IEnumerator ClosingSequence() {
        Phase = GamePhase.ClosingSequence;

        mainUIManager.StartSequence(null, false);


        // Mostly just money deductions and stuff
        bool doneCalculation = false;
        endDayPanel.transistion.Show(() => endDayPanel.Calculate());
        endDayPanel.Init(() => { doneCalculation = true; });
        
        yield return new WaitUntil(() => doneCalculation);

        endDayPanel.transistion.Hide();
        SetUpNextDay();
        yield break;
    }

    // Get ready for the next day
    private void SetUpNextDay() {
        Phase = GamePhase.AwaitNextDay;

        sun.GoToNextDay();

        StartEarlyMorningLoop();
    }
}
