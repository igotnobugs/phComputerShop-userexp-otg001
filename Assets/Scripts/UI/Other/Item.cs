using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int cost;
    public string itemName;

    public void bought()
    {
        if(GetComponentInParent<Shop>().currency >= cost)
        {
            GetComponentInParent<Shop>().currency -= cost;
            GetComponentInParent<Shop>().addItem(itemName);
        }
    }
}
