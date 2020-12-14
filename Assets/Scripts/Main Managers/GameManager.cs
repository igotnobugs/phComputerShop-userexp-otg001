using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

/* 
 * Phases handled by stateMachine
 * Open the ShopPhaseFSM for better visualization
 */

public class GameManager : Singleton<GameManager> 
{
    [Header("Setting Up")]
    [SerializeField] public TransistionUI transistion;
    [SerializeField] public OutsideLight sun;
    [SerializeField] public TextMeshProUGUI phaseInitial;
    [SerializeField] public TransistionUI openShopButton;
    [SerializeField] public RadialTimer radialTimer;
    [SerializeField] public StaffPanelManager staffPanel;
    [SerializeField] public MainUIManager mainUIManager;
    [SerializeField] public DialogueTrigger introDialogue = null;
    [SerializeField] public PopUpManager popUpManager = null;
    [SerializeField] public EndDayUI endDayPanel = null;

    [Header("Game Related")]
    [SerializeField] public Staff[] staffs;
    [SerializeField] public float shopOpenTime = 20.0f;

    public StoreData storeData;
    public static StoreData store;

    [Header("Scene Audio Manager")]
    public SceneAudioManager sceneAudio;

    [Header("Testing")]
    public bool showTutorial = true;
    public Transform door;
    public Customer testCustomer;
    public List<Customer> customers;

    public Animator stateMachine;

    private void Awake() {
        store = storeData;
        staffPanel = GetComponent<StaffPanelManager>();
        mainUIManager = GetComponent<MainUIManager>();
        stateMachine = GetComponent<Animator>();
    }

    public void StartTutorial(Action onEnd) {
        sceneAudio.Play("NightAmbience");
        StartCoroutine(NewGameSequence(onEnd));
    }

    private IEnumerator NewGameSequence(Action onEnd) {

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

        onEnd?.Invoke();
        yield break;
    }

    // When store is open, called by the button
    public void OpenStore() {
        sceneAudio.Stop("NightAmbience");
        sceneAudio.Play("DayAmbience");
        sceneAudio.Play("ShopAmbience");
        stateMachine.SetTrigger("openShopClicked");
    }

    public void StartCalculatingDayResult(Action onEnd) {
        StartCoroutine(ClosingSequence(onEnd));
    }

    // Do a sequence of closing work here
    private IEnumerator ClosingSequence(Action onEnd) {
        sceneAudio.Stop("ShopAmbience");
        sceneAudio.Stop("DayAmbience");
        sceneAudio.Play("NightAmbience");
        mainUIManager.StartSequence(null, false);

        // Mostly just money deductions and stuff
        bool doneCalculation = false;
        endDayPanel.transistion.Show(() => endDayPanel.Calculate());
        endDayPanel.Init(() => { doneCalculation = true; });
        
        yield return new WaitUntil(() => doneCalculation);

        endDayPanel.transistion.Hide();

        onEnd?.Invoke();
        yield break;
    }
}
