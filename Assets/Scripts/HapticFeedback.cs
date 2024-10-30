using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class HapticFeedback : MonoBehaviour
{

    public void TriggerRightHaptic(float amplitude, float duration)
    {
        var rightHand = InputSystem.GetDevice<UnityEngine.InputSystem.XR.XRController>(CommonUsages.RightHand);

        if (rightHand != null && rightHand is XRControllerWithRumble rightRumble)
        {
            rightRumble.SendImpulse(amplitude, duration);
        }
    }

    public void TriggerLeftHaptic(float amplitude, float duration)
    {
        var leftHand = InputSystem.GetDevice<UnityEngine.InputSystem.XR.XRController>(CommonUsages.LeftHand);

        if (leftHand != null && leftHand is XRControllerWithRumble leftRumble)
        {
            leftRumble.SendImpulse(amplitude, duration);
        }
    }
}