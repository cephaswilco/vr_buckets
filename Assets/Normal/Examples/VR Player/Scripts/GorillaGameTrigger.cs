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

    float time = 0f;

    [SerializeField]
    GorillaHoopHandler gorillaHoopHandler;

    private void Update()
    {
        time += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (time < 5f)
            return;

        RealtimeTransform ballRealtimeTransform = other.GetComponent<RealtimeTransform>();

        if (!ballRealtimeTransform.isOwnedLocallySelf)
            return;

        if (other.gameObject.layer == LayerMask.NameToLayer("Grabbable"))
        {
            scoreReporter.ReportScore(ballRealtimeTransform.ownerIDSelf, 1);
            gorillaBallGameController.ReportScore(gorillaHoopHandler.HoopID);
        }
    }
}
