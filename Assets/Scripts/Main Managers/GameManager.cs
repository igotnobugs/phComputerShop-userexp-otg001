using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

/* 
 * Phases handled by stateMachine
 * Open the ShopPhaseFSM for better visualization
 */

public class GameManager : Singleton<GameManager> 
{
    [Header("Setting Up")]
    public TransistionUI transistion;
    public OutsideLight sun;
    public TextMeshProUGUI phaseInitial;
    [SerializeField] public Button openShopButton;
    [SerializeField] public RadialTimer radialTimer;
    [SerializeField] public MainUIManager mainUIManager;
    [SerializeField] public DialogueTrigger introDialogue = null;
    [SerializeField] public DialogueTrigger tutorial2Dialogue = null;
    [SerializeField] public DialogueTrigger tutorial3Dialogue = null;
    [SerializeField] public EndDayUI endDayPanel = null;
    [SerializeField] public EmotionPopManager emoManager = null;

    [Header("Game Related")]
    [SerializeField] public float shopOpenTime = 20.0f;
    [SerializeField] public Staff[] staffs;
    [SerializeField] public Staff[] staffsInTheStore;
    
    public StoreData storeData;
    public static StoreData store;

    [Header("Scene Audio Manager")]
    public SceneAudioManager sceneAudio;

    [Header("Testing")]
    public bool showTutorial = true;
    public Transform door;

    public List<Customer> customersToSpawn;
    public static List<Customer> customersInStore = new List<Customer>();
    public static EmotionPopManager emo = null;

    public Animator StateMachine { private set; get; }
    private IEnumerator coroutine_CustomerSpawner;

    private void Awake() {
        store = storeData;
        mainUIManager = GetComponent<MainUIManager>();
        StateMachine = GetComponent<Animator>();
        emo = emoManager;
    }

    public void StartCalculatingDayResult(Action onEnd) {
        StartCoroutine(ClosingSequence(onEnd));
    }

    // Do a sequence of closing work here
    private IEnumerator ClosingSequence(Action onEnd) {

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

    public void StartSpawningCustomers() {
        coroutine_CustomerSpawner = SpawnCustomers(0.5f, 2.0f);
        StartCoroutine(coroutine_CustomerSpawner);
    }

    public void StopSpawningCustomers() {
        StopCoroutine(coroutine_CustomerSpawner);
    }

    private IEnumerator SpawnCustomers(float minRange = 0.5f, float maxRange = 0.5f) {
        for (; ; ) {
            int randomCustomer = UnityEngine.Random.Range(0, customersToSpawn.Count);

            Customer newCustomer = Instantiate(customersToSpawn[randomCustomer],
                door.position,
                door.rotation);
            newCustomer.door = door;

            customersInStore.Add(newCustomer);
            float randomRange = UnityEngine.Random.Range(minRange, maxRange);
            yield return new WaitForSeconds(randomRange);
        }
    }
}
