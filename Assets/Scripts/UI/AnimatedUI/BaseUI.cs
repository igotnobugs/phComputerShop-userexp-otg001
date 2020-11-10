using UnityEngine;

/* Nothing here but an events
 * Useful to keep track of what the player is doing with the UI
 * 
 */

public class BaseUI : MonoBehaviour 
{
    public delegate void UIEvent();

    // Still Animating    
    public event UIEvent OnActivating; // Active / Show / Pop   
    public event UIEvent OnDeactivating; // Deactive / Hide / Shrink

    // Finished Animating
    public event UIEvent OnActivated;
    public event UIEvent OnDeactivated;

    protected virtual void InvokeActivatingEvent() {
        OnActivating?.Invoke();
    }

    protected virtual void InvokeDeactivatingEvent() {
        OnDeactivating?.Invoke();
    }

    protected virtual void InvokeActivatedEvent() {
        OnActivated?.Invoke();
    }

    protected virtual void InvokeDeactivatedEvent() {
        OnDeactivated?.Invoke();
    }
}
