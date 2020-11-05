using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/* FOR UI
 * Makes something fade in/out with mouse position
 */

[RequireComponent(typeof(EventTrigger))]
public class UIFadeMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    private Color defaultColor;
    private Color showColor;
    private Image image = null;
    private RectTransform rect = null;

    private void Awake() {
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    private void Start() {
        defaultColor = image.color;
        showColor = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 1.0f);
    }


    public void OnPointerEnter(PointerEventData eventData) {
        LeanTween.color(rect, showColor, 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData) {
        LeanTween.color(rect, defaultColor, 0.5f);
    }
}
