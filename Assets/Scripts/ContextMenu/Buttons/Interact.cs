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

        if (SelectionManager.SelectedObject.TryGetComponent(out NPC npc)) {
            contextObject.Interact(npc);
        }
        contextManager.DisableContextMenu();
    }
}
