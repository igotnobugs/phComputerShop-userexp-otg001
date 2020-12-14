using System;
using UnityEngine;
using UnityEngine.EventSystems;

/* Can be clicked
 * 
 */

[RequireComponent(typeof(EventTrigger))]
public class ActiveUI : BaseUI, IPointerClickHandler
{
    public LTDescr TweenID { get; protected set; }

    [SerializeField] protected RectTransform rect;
    [SerializeField] protected Vector2 activePosition;
    [SerializeField] protected Vector2 unactivePosition;
    [SerializeField] protected float tweenDuration;
    [SerializeField] protected LeanTweenType activeTweenType;
    [SerializeField] protected LeanTweenType unactiveTweenType;
    [SerializeField] protected GameObject deactivateButton;
 
    public bool IsActive { get; protected set; }

    private SceneAudioManager audioManager;

    [Header("Sounds")]
    [SerializeField] protected bool allowSounds = false;
    [SerializeField] protected string activatingSound = "";
    [SerializeField] protected string deactivatingSound = "";

    protected override void Start() {
        base.Start();
        audioManager = FindObjectOfType<SceneAudioManager>();
        if (audioManager == null) allowSounds = false;
    }

    public LTDescr Active(Action onCompleteFunc = null) {
        gameObject.SetActive(true);
        InvokeActivatingEvent();
        IsActive = true;

        if (allowSounds && activatingSound != "") {
            audioManager.Play(activatingSound);
        }

        TweenID = LeanTween.move(rect, activePosition, tweenDuration);
        TweenID.setEase(activeTweenType);
        TweenID.setOnComplete(() => {
            InvokeActivatedEvent();
            onCompleteFunc?.Invoke();
        });
        
        return TweenID;
    }

    public LTDescr Deactive(Action onCompleteFunc = null) {
        InvokeDeactivatingEvent();
        IsActive = false;

        if (allowSounds && deactivatingSound != "") {
            audioManager.Play(deactivatingSound);
        }

        TweenID = LeanTween.move(rect, unactivePosition, tweenDuration);
        TweenID.setEase(unactiveTweenType);
        TweenID.setOnComplete(() => {
            InvokeDeactivatedEvent();
            onCompleteFunc?.Invoke();
        });
       
        return TweenID;
    }

    public virtual void OnPointerClick(PointerEventData eventData) {
        if (deactivateButton != null && IsActive) {
            RaycastResult result = eventData.pointerCurrentRaycast;
            if (result.gameObject == deactivateButton) {
                Deactive();
            }
        } else {
            Toggle();
        }     
    }

    public void Toggle() {
        if (!IsActive) {         
            Active();           
        }
        else {          
            Deactive();         
        }
    }
}
