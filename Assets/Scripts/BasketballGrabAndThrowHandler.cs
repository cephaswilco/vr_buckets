using Normal.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class BasketballGrabAndThrowHandler : MonoBehaviour
{
    [SerializeField]
    RealtimeView realtimeView;

    [SerializeField]
    RealtimeTransform realtimeTransform;

    public InputActionProperty handGripAction;
    float gripValue;
    float previousGripValue;
    float consecutiveGrip;
    float consecutiveGripThreshhold = 0.16f;

    [SerializeField]
    Transform baseRig;

    Rigidbody baseRigRB;

    [SerializeField]
    HapticFeedback hapticFeedback;

    bool gripped;

    Grabbable grabbedObject;

    UnityEngine.XR.InputDevice RightControllerDevice;

    Vector3 xrVelocity;

    int frameLength = 3;

    Queue<Vector3> ballVelocityFrames;

    Vector3 lastFrameAdded;

    [SerializeField]
    AnimationCurve playerPhysicsCurve;


    // Start is called before the first frame update
    void Awake()
    {
        baseRigRB = baseRig.GetComponent<Rigidbody>();
        ballVelocityFrames = new Queue<Vector3>(frameLength);
        RightControllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }


    private void FixedUpdate()
    {
        if (gripped)
        {
            RightControllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            RightControllerDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out xrVelocity);

            lastFrameAdded = xrVelocity;

            AddVelocityFrame(lastFrameAdded);
        }
    }

    // Update is called once per frame
    void Update()
    {
        gripValue = handGripAction.action.ReadValue<float>();
        Debug.Log("gripValue " + gripValue);
        Debug.Log("realtimeTransform " + realtimeTransform.ownerIDSelf);
        if (!realtimeTransform.isOwnedLocallySelf)
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


                Vector3 ballVelocityBeforeRotation = GetProcessedVelocity();

                Vector3 velocity = baseRig.rotation * ballVelocityBeforeRotation;

                Rigidbody grabbedObjectRigidBody = grabbedObject.GetComponent<Rigidbody>();
                grabbedObjectRigidBody.isKinematic = false;
                grabbedObjectRigidBody.AddForce(velocity * 2.5f, ForceMode.VelocityChange);

                xrVelocity = new Vector3();
                grabbedObject.Release();
                grabbedObject = null;

                //Clears frames for processing next throw
                ballVelocityFrames.Clear();
                lastFrameAdded = new Vector3();
            }
        }
        previousGripValue = gripValue;
    }

    public void AddVelocityFrame(Vector3 velocity)
    {
        if (ballVelocityFrames.Count >= frameLength)
        {
            ballVelocityFrames.Dequeue();
        }
        ballVelocityFrames.Enqueue(velocity);
    }

    public Vector3 GetProcessedVelocity()
    {

        // processedFrameVelocity takes the last 3 frames of velocity, with extra weight to the last frame of the controller before releasing the throw.
        Vector3 processedFrameVelocity = GetProcessedVelocityFromFrames();

        // processedPlayerVelocity represents the players current velocity
        Vector3 specialNormalizedPlayerVelocity;
        float playerMagnitude = GetCurrentPlayerVelocity().magnitude;
        if (playerMagnitude <= 1)
        {
            specialNormalizedPlayerVelocity = GetCurrentPlayerVelocity();
        }
        else
        {
            // There is a curve set up to transfer diminishing player velocity to the ball based on speed, as you go faster, adding equal player velocity to ball doesn't feel right (probably because no one actually moves this fast and throws a ball, and wind factor etc)
            float curveValue = playerPhysicsCurve.Evaluate(playerMagnitude);
            Debug.Log("Value: " + curveValue);
            specialNormalizedPlayerVelocity = GetCurrentPlayerVelocity() * curveValue * 0.25f;
        }

        Vector3 processedPlayerVelocity = specialNormalizedPlayerVelocity;

        Debug.Log("processedPlayerMagnitude: " + playerMagnitude);

        // The last frame of velocity again to give even more weight to it. 
        //Vector3 lastFrameVelocity = lastFrameAdded;

        // This should give the ball some player 
        Vector3 newProcessedVelocity = (processedFrameVelocity + processedPlayerVelocity);

        return newProcessedVelocity;
    }


    public Vector3 GetCurrentPlayerVelocity()
    {
        Vector3 playerVelocity = new Vector3();

        if (baseRigRB != null)
        {
            playerVelocity = baseRigRB.velocity;
        }

        Debug.Log("CurrentPlayerVelocity: " + playerVelocity);

        return playerVelocity;
    }

    public Vector3 GetProcessedVelocityFromFrames()
    {
        Vector3 currentFrames = new Vector3(0, 0, 0);

        foreach (Vector3 frame in ballVelocityFrames)
        {
            currentFrames += frame;
        }

        Vector3 averagedFrame = currentFrames / (float)ballVelocityFrames.Count;

        // Gives special weight the last frame thrown
        averagedFrame = (averagedFrame + lastFrameAdded) / 2.0f;

        return averagedFrame;
    }


    private void OnTriggerStay(Collider other)
    {
        if (!realtimeTransform.isOwnedLocallySelf)
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
