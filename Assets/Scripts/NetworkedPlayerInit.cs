using Normal.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NetworkedPlayerInit : MonoBehaviour
{

    GameObject PhysicsXRRig;
    RealtimeView realtimeView;

    [SerializeField]
    Transform LeftController;

    [SerializeField]
    Transform RightController;

    [SerializeField]
    Rigidbody PlayerRigidbody;

    PhysicsXRRigHelper physicsXRRig;

    Vector3 startPosition;

    ScoreReporter scoreReporter;

    int delayTickTime = 100;
    int delayTime = 0;
    bool delayFlag;

    void Awake()
    {
        realtimeView = GetComponent<RealtimeView>();
        scoreReporter = FindAnyObjectByType<ScoreReporter>();
        physicsXRRig = FindAnyObjectByType<PhysicsXRRigHelper>();
        realtimeView.didReplaceAllComponentModels += OnModelChanged;    
    } 
    
    // This sets up the Physics XR rig (Gorilla controllers) to the networked player, and also delays the initialization to avoid weird physics / forces from the xr physics hands sending player flying. 
    void SetPhysicsXRRig()
    {
        delayFlag = true;
        physicsXRRig.enabled = false;
        PlayerRigidbody.isKinematic = true;
        physicsXRRig.SetHandTargetLeft(LeftController);
        physicsXRRig.SetHandTargetRight(RightController);
        physicsXRRig.SetPlayerRigidbodyLeft(PlayerRigidbody);
        physicsXRRig.SetPlayerRigidbodyRight(PlayerRigidbody);
        physicsXRRig.Enable(true);
        startPosition = new Vector3(0, 2, realtimeView.ownerIDSelf * 2);
        transform.position = startPosition; 
        physicsXRRig.enabled = true;
        scoreReporter.ScoreChanged();
    }

    private void FixedUpdate()
    {

        // Count off 100 fixed updates worth of physics ticks before allowing movement, 2 seconds worth with default 0.02 fixed update.
        if (delayFlag)
        {
            delayTime++;
        }        

        if (delayTime > delayTickTime && delayFlag)
        {
            delayFlag = false;
            PlayerRigidbody.isKinematic = false;

        }
    }

    private void OnModelChanged(RealtimeView model)
    {
        if (realtimeView.isOwnedLocallySelf)
        {
            SetPhysicsXRRig();
        }
    }

}
