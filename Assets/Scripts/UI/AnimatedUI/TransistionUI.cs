using System;
using UnityEngine;

/* Hide/Show (ex. Move in to the screen coming from outside)
 */

public class TransistionUI : BaseUI 
{
    [SerializeField] protected RectTransform rect;
    [SerializeField] protected bool startShowed;
    [SerializeField] protected Vector2 hidePosition;
    [SerializeField] protected Vector2 showPosition;
    [SerializeField] protected float tweenDuration;
    [SerializeField] protected LeanTweenType hideTweenType;
    [SerializeField] protected LeanTweenType showTweenType;

    public LTDescr TweenID { get; protected set; }
    public bool IsActive { get; protected set; }

    protected virtual void Start() {
        if (startShowed) {
            rect.anchoredPosition = showPosition;
        }
        else {
            rect.anchoredPosition = hidePosition;
            gameObject.SetActive(false);
        }
    }

    public virtual LTDescr Show(Action onCompleteFunc = null) {
        gameObject.SetActive(true);
        InvokeActivatingEvent();
        IsActive = true;

        TweenID = LeanTween.move(rect, showPosition, tweenDuration);
        TweenID.setEase(showTweenType);
        TweenID.setOnComplete(() => {
            InvokeActivatedEvent();
            onCompleteFunc?.Invoke();
        });  

        return TweenID;
    }

    public virtual LTDescr Hide(Action onCompleteFunc = null) {
        InvokeDeactivatingEvent();

        TweenID = LeanTween.move(rect, hidePosition, tweenDuration);
        TweenID.setEase(showTweenType);       
        TweenID.setOnComplete(() => {
            InvokeDeactivatedEvent();
            onCompleteFunc?.Invoke();
            gameObject.SetActive(false);
            IsActive = false;
        });
      
        return TweenID;
    }

    public void ClickedShow() {
        Show();
    }

    public void HideShow() {
        Hide();
    }

    public void Toggle() {
        if (!IsActive) {
            Show();
        }
        else {
            Hide();
        }
    }
}
