using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{

    public Action<NetworkedBall> ReportTriggerEntered;
    public Action<NetworkedBall> ReportTriggerExit;


    void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<NetworkedBall>(out NetworkedBall ball);
        if (ball != null)
        {
            ReportTriggerEntered?.Invoke(ball);
        }
    }

    void OnTriggerExit(Collider other)
    {
        other.TryGetComponent<NetworkedBall>(out NetworkedBall ball);
        if (ball != null)
        {
            ReportTriggerExit?.Invoke(ball);
        }
    }

}
