using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/* Lower-Left UI tabs (Ledger and Shop)
 * Pops in and out
 * 
 * WIP - Only one must pop out
 */

[RequireComponent(typeof(EventTrigger), typeof(UITween))]
public class UITab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
    IPointerClickHandler {

    [Header("Peek Settings")]
    [SerializeField] private Vector3 peekDistance = new Vector3(0, 15.0f, 0);
    [SerializeField] private float peekTime = 0.5f;
    [SerializeField] private LeanTweenType peekType = LeanTweenType.easeOutCirc;

    private bool isShown;
    private UITween tween;

    private void Awake() {
        tween = GetComponent<UITween>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (isShown) return;
        tween.TweenTo(peekDistance, peekTime, peekType);
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (isShown) return;
        tween.TweenTo(Vector3.zero, peekTime, peekType);
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (!isShown) {
            isShown = true;
            tween.GoToRelativePosition();
        }
    }

    public void Toggle() {
        if (!isShown) {
            isShown = true;
            tween.GoToRelativePosition();
        }
        else {
            isShown = false;
            tween.GoToDefaultPosition();
        }
    }

}
