using Normal.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedBall : MonoBehaviour
{

    RealtimeTransform realtimeTransform;
    int currentPlayerID;


    private void Awake()
    {
        realtimeTransform = GetComponent<RealtimeTransform>();
    }

    public void SetCurrentPlayerID(int currentPlayerID)
    {
        this.currentPlayerID = currentPlayerID;
    }

    public int GetCurrentPlayerID()
    {
        Debug.Log("THIS IS THE PLAYER ID JUST LOGGED FROM NETWORKED BALL: " + realtimeTransform.ownerIDSelf);
        return realtimeTransform.ownerIDSelf;
    }

}
