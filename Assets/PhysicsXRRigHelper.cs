using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsXRRigHelper : MonoBehaviour
{
    [SerializeField]
    PhysicsHand physicsHandLeft;

    [SerializeField]
    PhysicsHand physicsHandRight;

    public void SetPlayerRigidbodyLeft(Rigidbody playerRigidbody)
    {
        physicsHandLeft.SetPlayerRigidbody(playerRigidbody);
    }

    public void SetPlayerRigidbodyRight(Rigidbody playerRigidbody)
    {
        physicsHandRight.SetPlayerRigidbody(playerRigidbody);
    }

    public void SetHandTargetLeft(Transform hand)
    {
        physicsHandLeft.SetTarget(hand);
    }

    public void SetHandTargetRight(Transform hand)
    {
        physicsHandRight.SetTarget(hand);
    }

    public void Enable(bool set)
    {
        physicsHandRight.SetUpdateEnabled(set);
    }
}
