using System;
using UnityEngine;
using UnityEngine.EventSystems;

/* Pops in when hovered
 */

[RequireComponent(typeof(EventTrigger))]
public class HoveredUI : BaseUI,  IPointerEnterHandler, IPointerExitHandler
{
    public LTDescr TweenID; 
    public ActiveUI ActiveUI { get; protected set; }

    [SerializeField] protected RectTransform rect;
    [SerializeField] protected Vector2 hoveredRelativePosition;
    [SerializeField] protected float tweenDuration;
    [SerializeField] protected LeanTweenType hoverTweenType;
    [SerializeField] protected LeanTweenType unHoverTweenType;

    private Vector2 normalPosition;
    public bool IsActive { get; protected set; }

    protected virtual void Awake() {
        normalPosition = rect.anchoredPosition;
        ActiveUI = GetComponent<ActiveUI>();
    }

    protected virtual void OnHovered(Action onCompleteFunc = null) {
        InvokeActivatingEvent();
        Vector3 newPos = normalPosition + hoveredRelativePosition;

        TweenID = LeanTween.move(rect, newPos, tweenDuration);
        TweenID.setEase(hoverTweenType);
        TweenID.setOnComplete(() => {
            InvokeActivatedEvent();
            onCompleteFunc?.Invoke();
        });
    }

    protected virtual void OnUnhovered(Action onCompleteFunc = null) {
        InvokeDeactivatingEvent();

        TweenID = LeanTween.move(rect, normalPosition, tweenDuration);
        TweenID.setEase(unHoverTweenType);
        TweenID.setOnComplete(() => {
            InvokeDeactivatedEvent();
            onCompleteFunc?.Invoke();
        });

    }

    public virtual void OnPointerEnter(PointerEventData eventData) {
        if (ActiveUI != null && ActiveUI.IsActive)
            return;

        OnHovered();
    }

    public virtual void OnPointerExit(PointerEventData eventData) {
        if (ActiveUI != null && ActiveUI.IsActive)
            return;

        OnUnhovered();
    }

    // For unique cases
    public virtual void Toggle() {
        if (!IsActive) {
            OnHovered();
            IsActive = true;
        }
        else {
            OnUnhovered();
            IsActive = false;
        }
    }

    public void Pop() {
        if (!IsActive) {
            OnHovered();
            IsActive = true;
        }
    }

    public void Shrink() {
        if (IsActive) {
            OnUnhovered();
            IsActive = false;
        }
    }
}
