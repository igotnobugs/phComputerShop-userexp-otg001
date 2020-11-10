using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

/* Applied to a Prefab Staff Button
 * Created by the StaffPanelManager
 * Clickable at the Staff Panel
 */

public class StaffButton  : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image portrait = null;
    [SerializeField] private TextMeshProUGUI shortNameText = null;
    [SerializeField] private Staff trackedStaff = null;

    public void Init(Staff staffToTrack, Sprite displayedPortrait, string shortName) {
        portrait.sprite = displayedPortrait;
        shortNameText.text = shortName;
        trackedStaff = staffToTrack;
    }

    public void OnClick() {
        SelectionManager.SetObjectAsSelected(trackedStaff.gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        trackedStaff.Hovered();
    }

    public void OnPointerExit(PointerEventData eventData) {
        trackedStaff.Unhovered();
    }
}
