using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class ObjectGrabber : MonoBehaviour
{

    public InputActionProperty handGripAction;
    float gripValue;
    float previousGripValue;
    float consecutiveGrip;
    float consevutiveGripThreshhold = 0.16f;


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
        gripValue = handGripAction.action.ReadValue<float>();

        if (grabbedObject != null)
        {
            grabbedObject.transform.position = transform.position;
            grabbedObject.transform.rotation = transform.rotation;
        }

        if (gripped)
        {
   
            consecutiveGrip += previousGripValue - gripValue; 

            if (consecutiveGrip > consevutiveGripThreshhold)
            {
                gripValue = 0f;
                consecutiveGrip = 0f;
            }

            if (gripValue <= 0f)
            {
                gripped = false;

                if (RightControllerDevice == null)
                {
                    RightControllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
                }

                RightControllerDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out xrVelocity);
                Vector3 velocity = xrVelocity;
                Debug.Log("Velocity: "  + velocity);
                Rigidbody grabbedObjectRigidBody = grabbedObject.GetComponent<Rigidbody>();
                grabbedObjectRigidBody.isKinematic = false;
                grabbedObjectRigidBody.AddForce(velocity * 2f, ForceMode.VelocityChange);
                //grabbedObject.Release();

                xrVelocity = new Vector3();

                grabbedObject = null;
            }
        }
        previousGripValue = gripValue;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!gripped)
        {
            if (RightControllerDevice == null)
            {
                RightControllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            }
            Grabbable grabbable = other.GetComponent<Grabbable>();
            if (grabbable)
            {
                if (gripValue > 0)
                {
                    gripped = true;
                    grabbedObject = grabbable.Grab(this.transform);
                    Debug.Log("Kinematic true");
                    grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
    }
}
