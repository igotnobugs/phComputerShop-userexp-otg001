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

    private static Transform staffPanelToUse;
    private static Button staffButtonPrefabToUse;
    private static List<StaffButton> staffButtons = new List<StaffButton>();

    private void Awake() {
        if (staffPanel == null) {
            Debug.Log("Staff Panel is not assigned.");
        } else {
            staffPanelToUse = staffPanel;
        }

        if (staffButtonPrefab == null) {
            Debug.Log("staff Button Prefab is not assigned.");
        } else {
            staffButtonPrefabToUse = staffButtonPrefab;
        }
    }

    public static void CreateStaffButton(Staff staff) {
        Button newButton = Instantiate(staffButtonPrefabToUse, staffPanelToUse);
        StaffButton newStaffButton = newButton.GetComponent<StaffButton>();

        if (newStaffButton != null && staff.attributes != null) {
            newStaffButton.Init(staff, staff.attributes.portrait, staff.attributes.initials);
            staffButtons.Add(newStaffButton);
        }
        else {
            if (newStaffButton != null)
                Debug.Log(staffButtonPrefabToUse + " has no StaffButton component.");

            if (staff.attributes != null)
                Debug.Log(staff + " has no Data.");
        }     
    }
}
