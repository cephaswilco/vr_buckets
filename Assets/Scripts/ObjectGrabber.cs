using Normal.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class ObjectGrabber : MonoBehaviour
{
    [SerializeField]
    RealtimeView realtimeView;
    public InputActionProperty handGripAction;
    float gripValue;
    float previousGripValue;
    float consecutiveGrip;
    float consecutiveGripThreshhold = 0.16f;

    [SerializeField]
    Transform baseRig;

    [SerializeField]
    HapticFeedback hapticFeedback;

    bool gripped;

    Grabbable grabbedObject;

    UnityEngine.XR.InputDevice RightControllerDevice;

    Vector3 xrVelocity;

    // Start is called before the first frame update
    void Awake()
    {
        RightControllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    // Update is called once per frame
    void Update()
    {
        if (!realtimeView.isOwnedLocallySelf)
        {
            return;
        }

        gripValue = handGripAction.action.ReadValue<float>();

        if (grabbedObject != null)
        {
            grabbedObject.transform.position = transform.position;
            grabbedObject.transform.rotation = transform.rotation;
        }

        if (gripped)
        {
   
            consecutiveGrip += previousGripValue - gripValue; 

            if (consecutiveGrip > consecutiveGripThreshhold)
            {
                gripValue = 0f;
                consecutiveGrip = 0f;
            }

            if (gripValue <= 0f)
            {
                hapticFeedback.TriggerRightHaptic(0.5f, 0.2f);
                gripped = false;

                RightControllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

                RightControllerDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out xrVelocity);
                Vector3 velocity = baseRig.rotation * xrVelocity;
                Debug.Log("Velocity: "  + velocity);
                Rigidbody grabbedObjectRigidBody = grabbedObject.GetComponent<Rigidbody>();
                grabbedObjectRigidBody.isKinematic = false;
                grabbedObjectRigidBody.AddForce(velocity * 3f, ForceMode.VelocityChange);

                xrVelocity = new Vector3();
                grabbedObject.Release();
                grabbedObject = null;
            }
        }
        previousGripValue = gripValue;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!realtimeView.isOwnedLocallySelf)
        {
            return;
        }

        if (!gripped)
        {
            if (RightControllerDevice == null)
            {
                RightControllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            }
            Grabbable grabbable = other.GetComponent<Grabbable>();
            BallScoreId networkedBall = other.GetComponent<BallScoreId>();
            if (grabbable && networkedBall)
            {
                if (gripValue > 0)
                {
                    gripped = true;
                    grabbedObject = grabbable.Grab(this.transform, realtimeView);
                    if (grabbedObject != null)
                    {
                        hapticFeedback.TriggerRightHaptic(0.7f, 0.35f);
                        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                        networkedBall.SetCurrentPlayerID(realtimeView.ownerIDSelf);
                    }
                 
                }
            }
        }
    }
}
