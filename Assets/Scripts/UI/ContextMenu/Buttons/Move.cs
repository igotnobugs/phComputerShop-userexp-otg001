using UnityEngine;

/* Same as clicking a staff and click anywhere to move
 * 
 */

public class Move : MonoBehaviour, IContextButton
{
    private ContextMenuManager contextManager;

    private void Awake() {
        GameObject go = GameObject.FindGameObjectWithTag("Manager");
        contextManager = go.GetComponent<ContextMenuManager>();
    }

    public void Enable() {
        if (SelectionManager.SelectedObject != null && 
            SelectionManager.HoveredObject == null) {
            if (SelectionManager.SelectedObject.tag == "Staff") {
                gameObject.SetActive(true);
                return;
            }
        }
        gameObject.SetActive(false);
    }

    public void OnClick() {
        Staff staff = SelectionManager.SelectedObject.GetComponent<Staff>();
        staff.MoveToGrid(ContextMenuManager.ContextMenuGridPosition);
        contextManager.DisableContextMenu();
    }
}
