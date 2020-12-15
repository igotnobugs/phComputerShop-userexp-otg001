using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Customer : NPC 
{
    public Transform door;

    public int budget;
    public float overPayChance;
    public float timeAtCounter; 
    public int timeToUse; 

    protected Counter counter;
    protected Computer computerToUse = null;

    private PersonalAudio personalAudio;

    public Action TalkAtCounterAction;
    public int numberInLine;

    public bool isLining;

    protected override void Awake() {
        base.Awake();
        personalAudio = GetComponent<PersonalAudio>();
        TalkAtCounterAction = () => StartCoroutine(StartTalking());
    }

    protected override void Start() {
        base.Start();
        CheckComputerAvailablity();
    }

    public void CheckComputerAvailablity() {
        Computer[] computers = FindObjectsOfType<Computer>();
        for (int i = 0; i < computers.Length; i++) {
            if (computers[i].CanBeUsed()) {
                LineUpAtCounter();
                return;
            }
        }
        
        LeaveTheStore(); //None available
    }

    private void LineUpAtCounter() {
        counter = FindObjectOfType<Counter>();
        if (counter == null) LeaveTheStore(); // No Counter

        if (counter.amountCustomerLining < 3) {      
            numberInLine = counter.amountCustomerLining;
            if (numberInLine > 0) {
                counter.CustomerDonePaying += MoveUpTheLine;
            }
            isLining = true;
            counter.LineUp(this);
            return;
        }

        // Line is too long
        LeaveTheStore();
    }

    private void MoveUpTheLine() {
        numberInLine--;
        int line = numberInLine;
        if (line < 0) line = 0;

        Vector3 interactDestination = GridCursor.WorldToGrid(counter.line[line].position);
        if (line <= 0) {
            MoveToGrid(interactDestination, () => {
                TalkAtCounterAction();
            });
        } else {
            MoveToGrid(interactDestination);
        }
    }

    private IEnumerator StartTalking() {
        counter.CustomerDonePaying -= MoveUpTheLine;

        yield return new WaitUntil(() => counter.isOccupied);

        (counter.user as Staff).isBusy = true;
        Computer[] computers = FindObjectsOfType<Computer>();
        while (computerToUse == null) { 
            for (int i = 0; i < computers.Length; i++) {
                if (computers[i].CanBeUsed()) {
                    computerToUse = computers[i];
                    break;
                }
            }
            yield return null;
        }
        yield return new WaitUntil(() => computerToUse != null);

        GameManager.emo.PopEmotion("question", transform, new Vector2(0, 2));

        float timeModified = 100.0f / counter.efficiency;
        timeAtCounter *= timeModified;

        yield return new WaitForSeconds(timeAtCounter);

        if (budget < GameManager.store.price) {
            if (UnityEngine.Random.Range(0, 1.0f) > overPayChance) {
                GameManager.emo.PopEmotion("ouch", transform, new Vector2(0, 2));
                LeaveTheStore();
                yield break;
            }
        }

        audioManager.Play("CashRegister");

        (counter.user as Staff).isBusy = false;
        (counter.user as Staff).attributes.DrainEnergyDefault();


        computerToUse.isOccupied = true;
        GameManager.emo.PopEmotion("happy", transform, new Vector2(0, 2));        
        GameManager.store.AddEarnings(GameManager.store.price);

        isLining = false;
        counter.CustomerDone();
        GoToComputer();
        yield break;
    }

    private void GoToComputer() {
        Interact(computerToUse, () => {
            StartCoroutine(InteractingComputer());
        });
    }

    private IEnumerator InteractingComputer() {
        //Do stuff
     
        yield return new WaitForSeconds(timeToUse);

        if (computerToUse.allowBrokenAfterUse) {
            float chance = UnityEngine.Random.Range(0, 100.0f);
            if (chance < clumsiness) {
                computerToUse.SetBroken();
            }
        }
        //

        LeaveTheStore();
        yield break;
    }

    public void LeaveTheStore() {
        //Customer still lining up
        if (isLining) {
            counter.CustomerDonePaying -= MoveUpTheLine;
            counter.amountCustomerLining--;
        }

        StopAllCoroutines();

        Vector3 doorGridPosition = GridCursor.WorldToGrid(door.position);
        MoveToGrid(doorGridPosition, () => {
            GameManager.customersInStore.Remove(this);
            DestroyMe(); //Prevents unity from destroying the pathfinding script! wtf
        });
    }

    public void DestroyMe() {
        Destroy(gameObject);
    }
}
