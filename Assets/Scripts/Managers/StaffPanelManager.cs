using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* The bottom UI panel, where you can click the staff
 */

public class StaffPanelManager : Singleton<StaffPanelManager> 
{
    public Transform staffPanel = null;
    public Button staffButtonPrefab = null;

    public List<StaffButton> staffButtons = new List<StaffButton>();

    private void Awake() {
        if (staffPanel == null)
            Debug.Log("Staff Panel is not assigned.");

        if (staffButtonPrefab == null)
            Debug.Log("staff Button Prefab is not assigned.");    
    }

    public void CreateStaffButton(Staff staff) {
        Button newButton = Instantiate(staffButtonPrefab, staffPanel);
        StaffButton newStaffButton = newButton.GetComponent<StaffButton>();

        if (newStaffButton != null && staff.attributes != null) {
            newStaffButton.Init(staff, staff.attributes.portrait, staff.attributes.initials);
            staffButtons.Add(newStaffButton);
        }
        else {
            if (newStaffButton != null)
                Debug.Log(staffButtonPrefab + " has no StaffButton component.");

            if (staff.attributes != null)
                Debug.Log(staff + " has no Data.");
        }     
    }
}
