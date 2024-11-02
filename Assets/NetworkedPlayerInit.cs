using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedPlayerInit : MonoBehaviour
{

    [SerializeField]
    GameObject PhysicsXRRigPrefab;

    [SerializeField]
    Transform LeftController;

    [SerializeField]
    Transform RightController;

    [SerializeField]
    Rigidbody PlayerRigidbody;

    PhysicsXRRigHelper physicsXRRig;

    void Awake()
    {
        physicsXRRig = Instantiate(PhysicsXRRigPrefab).GetComponent<PhysicsXRRigHelper>();

        physicsXRRig.SetHandTargetLeft(LeftController);
        physicsXRRig.SetHandTargetRight(RightController);
        physicsXRRig.SetPlayerRigidbodyLeft(PlayerRigidbody);
        physicsXRRig.SetPlayerRigidbodyRight(PlayerRigidbody);
        physicsXRRig.Enable(true);
    }

}
