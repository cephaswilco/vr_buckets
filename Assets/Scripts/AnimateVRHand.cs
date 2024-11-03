using Normal.Realtime;
using UnityEngine;
using UnityEngine.InputSystem;


public class AnimateVRHand : MonoBehaviour
{

    [SerializeField]
    RealtimeView realtimeView;

    [SerializeField]
    RealtimeTransform realtimeTransform;

    public InputActionProperty handPinchAnimationAction;
    public InputActionProperty handGripAnimationAction;
    public Animator handAnimator;

    float pinchValue;
    float gripValue;

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (realtimeTransform.isOwnedLocallySelf)
        {
            pinchValue = handPinchAnimationAction.action.ReadValue<float>();
            gripValue = handGripAnimationAction.action.ReadValue<float>();
            handAnimator.SetFloat("Trigger", pinchValue);
            handAnimator.SetFloat("Grip", gripValue);
        }   
    }
}
