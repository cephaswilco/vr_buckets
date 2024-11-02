using Normal.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



    void Awake()
    {
        realtimeView = GetComponent<RealtimeView>();
        realtimeView.didReplaceAllComponentModels += OnModelChanged;
        physicsXRRig = FindAnyObjectByType<PhysicsXRRigHelper>();
    }


   
    void SetPhysicsXRRig()
    {
        physicsXRRig.SetHandTargetLeft(LeftController);
        physicsXRRig.SetHandTargetRight(RightController);
        physicsXRRig.SetPlayerRigidbodyLeft(PlayerRigidbody);
        physicsXRRig.SetPlayerRigidbodyRight(PlayerRigidbody);
        physicsXRRig.Enable(true);
    }

    private void OnModelChanged(RealtimeView model)
    {
        if (realtimeView.isOwnedLocallySelf)
        {
            SetPhysicsXRRig();
        }
    }

}
