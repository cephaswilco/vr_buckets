using Normal.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaGameTrigger : MonoBehaviour
{
    [SerializeField]
    ScoreReporter scoreReporter;

    [SerializeField]
    GorillaBallGameController gorillaBallGameController;

    [SerializeField]
    GorillaHoopHandler gorillaHoopHandler;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabbable"))
        {
            Debug.Log("Basket Entered");
        }

        RealtimeTransform ballRealtimeTransform = other.GetComponent<RealtimeTransform>();

        if (ballRealtimeTransform == null)
        {
            return;
        }

        if (!ballRealtimeTransform.isOwnedLocallySelf)
            return;

        if (other.gameObject.layer == LayerMask.NameToLayer("Grabbable"))
        {
            scoreReporter.ReportScore(ballRealtimeTransform.ownerIDSelf, 1);
            gorillaBallGameController.ReportScore(gorillaHoopHandler.HoopID);
        }
    }
}
