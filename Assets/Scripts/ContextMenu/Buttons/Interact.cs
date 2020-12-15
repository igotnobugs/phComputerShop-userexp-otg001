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
        if (SelectionManager.SelectedObject == null) return;
        if (SelectionManager.HoveredObject == null) return;

        if (SelectionManager.SelectedObject.TryGetComponent(out Staff staff)) { 
            if (SelectionManager.HoveredObject.tag == "Furniture") {
                gameObject.SetActive(true);

                contextObject = SelectionManager.HoveredObject.GetComponent<Furniture>();
            }
        } else {
            gameObject.SetActive(false);
        }     
    }

    public void OnClick() {       

        if (SelectionManager.SelectedObject.TryGetComponent(out Staff staffNpc)) {
            if (contextObject.isBroken) {
                staffNpc.Interact(contextObject, () => {
                    staffNpc.StaffFix(contextObject);
                });
            } else {
                staffNpc.Interact(contextObject);
            }
        }
        contextManager.DisableContextMenu();
    }
}
