using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rent : MonoBehaviour
{
    private int rent = 900;
    public Text rentText;

    void Update()
    {
        rentText.text = "Rent : ₱" + rent;
    }
}
