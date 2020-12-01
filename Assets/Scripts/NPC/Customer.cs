using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Find a counter, line up, pay money, go to available computer
 * After certain time, leave
 */

public class Customer : NPC 
{
    public Transform door;

    public int amountToPay; //How much to pay
    public float timeAtCounter; //How long it talks at the counter

    public int timeToUse; //How long to use the computer

    protected Counter counter;
    protected Computer computerToUse;
    public List<Customer> costumerList;

    private AudioManager audioManager;

    private void Start() {
        audioManager = FindObjectOfType<AudioManager>();

        //Find a counter
        counter = FindObjectOfType<Counter>();


        // Go to counter
        if (counter != null) {
            counter.InteractAsCustomer(this as NPC, () => {
                StartCoroutine(StartTalking());
            });
        }
        else {
            Debug.Log("Shop has no counter?");
        }
    }

    private IEnumerator StartTalking() {
        //Do stuff maybe some effects?

        yield return new WaitUntil(() => counter.isOccupied);
      
        yield return new WaitForSeconds(timeAtCounter);

        // PAY
        audioManager.Play("CashRegister");
        GameManager.store.AddMoney(amountToPay);

        GoToComputer();
        yield break;
    }

    private void GoToComputer() {
        
        Computer[] computers = FindObjectsOfType<Computer>();
        for (int i = 0; i < computers.Length; i++) {
            if (!computers[i].isOccupied) {
                computerToUse = computers[i];
                break;
            }
        }

        // Should check this first before lining up but whatever
        if (computerToUse == null) {
            Debug.Log("No computers available.");
            LeaveTheStore();
        }
        else {
            computerToUse.Interact(this as NPC, () => {
                Debug.Log("Customer interacted!");
                StartCoroutine(InteractingComputer());
            });
        }
    }

    private IEnumerator InteractingComputer() {
        //Do stuff

        yield return new WaitForSeconds(timeToUse);

        Debug.Log("Customer done!");
        LeaveTheStore();
        yield break;
    }

    public void LeaveTheStore() {
        Debug.Log("Leaving the store!");

        StopAllCoroutines();
        Vector3 doorGridPosition = GridCursor.WorldToGrid(door.position);

        MoveToGrid(doorGridPosition, () => {
            costumerList.Remove(this);
            Destroy(gameObject);
        });
    }
}
