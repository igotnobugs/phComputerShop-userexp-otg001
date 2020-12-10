using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Clipboard : MonoBehaviour 
{
    [SerializeField] private TextMeshProUGUI fullname = null;
    [SerializeField] private TextMeshProUGUI nickanme = null;

    [SerializeField] private Image energy = null;
    [SerializeField] private Image social = null;
    [SerializeField] private Image technical = null;

    private void Update() {
        if (SelectionManager.SelectedObject != null) {
            if (SelectionManager.SelectedObject.TryGetComponent(out Staff staff)) {
                StaffObject staffdata = staff.attributes;

                fullname.text = staffdata.fullname;
                nickanme.text = staffdata.nickname;

                energy.fillAmount = staffdata.energy / 100.0f;
                social.fillAmount = staffdata.social / 100.0f;
                technical.fillAmount = staffdata.technical / 100.0f;
                return;
            }           
        }

        fullname.text = "No staff seleced";
        nickanme.text = "";

        energy.fillAmount = 0;
        social.fillAmount = 0;
        technical.fillAmount = 0;
    }

}
