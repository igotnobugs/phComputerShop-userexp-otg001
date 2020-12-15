using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Staff Data")]
public class StaffObject : ScriptableObject
{
    public string fullname;
    public string nickname;
    public string initials;

    public Sprite portrait;

    public int energy;
    public int social;
    public int technical;

    public int wage;
    public int energyDrain = 10;

    public void DrainEnergyDefault() {
        float wageMult = GameManager.store.wageMult;
        int energyToDrain = (int)(energyDrain * (1 / wageMult));
        DrainEnergy(energyToDrain);
    }

    public void DrainEnergy(int value) {
        energy -= value;
        if (energy < 0) {
            energy = 0;
        }
    }
}
