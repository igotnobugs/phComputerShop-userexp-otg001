using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour 
{
    [SerializeField] private Popup popUpPrefab = null;

    public BaseUI ledger;

    public Canvas gameUI;

    private void Start() {
        CreatePopUp("Test", "Content", ledger.transform.position);      
    }


    private void Update() {
        
    }

    public void CreatePopUp(string title, string text, Vector2 location) {
        Popup newPopup = Instantiate(popUpPrefab, gameUI.transform);
        newPopup.Init(title, text);
        newPopup.MoveTo(location);
    }
}
