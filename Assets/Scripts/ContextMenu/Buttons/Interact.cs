using UnityEngine;

/* Command for staffs to interact a specific furniture
 * 
 */

public class Interact : MonoBehaviour, IContextButton 
{
    private ContextMenuManager contextManager;
    private Furniture contextObject;

    private void Awake() {
        contextManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ContextMenuManager>();
    }

    public void Enable() {
        if (SelectionManager.SelectedObject != null) {
            if (SelectionManager.SelectedObject.tag == "Staff" && 
                SelectionManager.HoveredObject.tag == "Furniture") {
                gameObject.SetActive(true);

                contextObject = SelectionManager.HoveredObject.GetComponent<Furniture>();
            }
        } else {
            gameObject.SetActive(false);
        }     
    }

    public void OnClick() {       
        Staff staff = SelectionManager.SelectedObject.GetComponent<Staff>();
        staff.MoveToGrid(contextObject.interactDestination);

        contextManager.DisableContextMenu();
    }
}
