using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/* Changes color when hovered
 * Ignores position
 * Usually used when clicked it activates another UI
 */

public class HoveredColorUI : HoveredUI 
{
    [Header("Hover Color Settings")]
    [SerializeField] protected Image image;
    [SerializeField] protected Color defaultColor;
    [SerializeField] protected Color showColor;

    //if this game object is enable
    public GameObject trackGameObject;
    
    protected override void Awake() {
        rect = GetComponent<RectTransform>();
        image.color = defaultColor;
    }

    protected override void OnHovered(Action onCompleteFunc = null) {
        InvokeActivatingEvent();

        TweenID = LeanTween.color(rect, showColor, tweenDuration);
        TweenID.setEase(hoverTweenType);
        TweenID.setOnComplete(() => {
            InvokeActivatedEvent();
            onCompleteFunc?.Invoke();
        });
    }

    protected override void OnUnhovered(Action onCompleteFunc = null) {
        InvokeDeactivatingEvent();

        TweenID = LeanTween.color(rect, defaultColor, tweenDuration);
        TweenID.setEase(unHoverTweenType);
        TweenID.setOnComplete(() => {
            InvokeDeactivatedEvent();
            onCompleteFunc?.Invoke();
        });

    }

    public override void OnPointerEnter(PointerEventData eventData) {
        if (trackGameObject != null && trackGameObject.activeSelf)
            return;

        OnHovered();
    }

    public override void OnPointerExit(PointerEventData eventData) {
        if (trackGameObject != null && trackGameObject.activeSelf)
            return;

        OnUnhovered();
    }

    public override void Toggle() {
        if (!IsActive) {
            image.color = showColor;
            IsActive = true;
        }
        else {
            image.color = defaultColor;
            IsActive = false;
        }
    }


}
