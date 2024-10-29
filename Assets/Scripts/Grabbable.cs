using Normal.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grabbable : MonoBehaviour, IGrabbable
{

    RealtimeTransform realtimeTransform;
    NetworkedGrabbed networkGrabbed;

    void Awake()
    {
        realtimeTransform = GetComponent<RealtimeTransform>();
        networkGrabbed = GetComponent<NetworkedGrabbed>();
    }

    void Update()
    {

    }

    public Grabbable Grab(Transform grabberTransform, RealtimeView realtimeView)
    {
        // If the item isn't grabbed, can grab. 
        // -1 means no one has grabbed, self means local currently owns it
        if (networkGrabbed.playerID == -1 || networkGrabbed.playerID == realtimeView.ownerIDSelf)
        {
            networkGrabbed.SetPlayerID(realtimeTransform.realtimeView.ownerIDSelf);
            realtimeTransform.RequestOwnership();
            return this;
        }

        return null;
    }

    public void Release()
    {
        if (realtimeTransform.isOwnedLocallySelf)
        {
            // No one has the ball grabbed
            networkGrabbed.SetPlayerID(-1);
        }
    }


}


public interface IGrabbable
{
    public Grabbable Grab(Transform transform, RealtimeView realtimeView);
}