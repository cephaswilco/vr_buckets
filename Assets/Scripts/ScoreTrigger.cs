using Normal.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{

    public Action<BallScoreId> ReportTriggerEntered;
    public Action<BallScoreId> ReportTriggerExit;

    Realtime realtime;

    int localID = -1;


    private void Awake()
    {
        realtime = FindObjectOfType<Realtime>();
        SetLocalID();   
    }



    void SetLocalID()
    {
        realtime = FindObjectOfType<Realtime>();
        RealtimeAvatarManager avatarManager = realtime.GetComponent<RealtimeAvatarManager>();
        if (avatarManager != null && avatarManager.localAvatar != null)
        {
            localID = avatarManager.localAvatar.realtimeView.ownerIDSelf;
            Debug.Log("LOCAL ID RROM REALTIME: " + localID);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (localID == -1)
        {
            SetLocalID();
        }

        other.TryGetComponent<BallScoreId>(out BallScoreId ball);
        if (ball != null)
        {
            if (ball.GetCurrentPlayerID() == localID)
            {
                ReportTriggerEntered?.Invoke(ball);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (localID == -1)
        {
            SetLocalID();
        }

        other.TryGetComponent<BallScoreId>(out BallScoreId ball);
        if (ball != null)
        {
            // This only reports balls that scored
            if (ball.GetCurrentPlayerID() == localID)
            {
                ReportTriggerExit?.Invoke(ball);
            }         
        }
    }

}
