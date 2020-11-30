using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Enables the gameObject when tab is activated
 * 
 * 
 */

public class TabToggler : MonoBehaviour 
{
    public BaseUI transistion;
    public GameObject content;

    private void Start() {
        transistion.OnActivating += OnActivating;
        transistion.OnDeactivated += OnDeactivated;
    }

    private void OnActivating() {
        content.SetActive(true);
    }

    private void OnDeactivated() {
        content.SetActive(false);
    }

    private void HideContent() {
        content.SetActive(false);
    }
}
