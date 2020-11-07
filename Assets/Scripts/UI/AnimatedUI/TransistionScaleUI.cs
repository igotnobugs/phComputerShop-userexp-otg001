using System;

/* Transistions in by scaling
 * 
 * Positions becomes scaling vectors
 */

public class TransistionScaleUI : TransistionUI 
{
    protected override void Start() {
        if (startShowed) {
            rect.localScale = showPosition;
        }
        else {
            rect.localScale = hidePosition;
            gameObject.SetActive(false);
        }
    }

    public override LTDescr Show(Action onCompleteFunc = null) {
        gameObject.SetActive(true);
        InvokeActivatingEvent();
        IsActive = true;

        TweenID = LeanTween.scale(rect, showPosition, tweenDuration);
        TweenID.setEase(showTweenType);     
        TweenID.setOnComplete(() => {
            InvokeActivatedEvent();
            onCompleteFunc?.Invoke();
        });
       
        return TweenID;
    }

    public override LTDescr Hide(Action onCompleteFunc = null) {
        InvokeDeactivatingEvent();

        TweenID = LeanTween.scale(rect, showPosition, tweenDuration);
        TweenID.setEase(showTweenType);
        TweenID.setOnComplete(() => {
            InvokeDeactivatedEvent();
            onCompleteFunc?.Invoke();
            gameObject.SetActive(false);
            IsActive = false;
        });
      
        return TweenID;
    }
}
