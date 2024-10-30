using Normal.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grabbable : MonoBehaviour, IGrabbable
{

    RealtimeView realtimeView;
    RealtimeTransform realtimeTransform;
    Grabbed networkGrabbed;

    void Awake()
    {
        realtimeView = GetComponent<RealtimeView>();
        realtimeTransform = GetComponent<RealtimeTransform>();
        networkGrabbed = GetComponent<Grabbed>();
    }

    void Update()
    {

    }

    public Grabbable Grab(Transform grabberTransform, RealtimeView grabbingRealtimeView)
    {

        Debug.Log("Grabbed ball belongs to " + networkGrabbed.GetPlayerID() + " and player " + grabbingRealtimeView.ownerIDSelf + " is trying to grab it");

        // If the item isn't grabbed, can grab. 
        // -1 means no one has grabbed, self means local currently owns it
        if (networkGrabbed.GetPlayerID() == -1 || networkGrabbed.GetPlayerID() == grabbingRealtimeView.ownerIDSelf)
        {
            Debug.Log("Grabbed ball belongs to " + networkGrabbed.GetPlayerID() + " and player " + grabbingRealtimeView.ownerIDSelf + " is trying to grab it --- Success!");

            networkGrabbed.SetPlayerID(grabbingRealtimeView.ownerIDSelf);
            realtimeTransform.RequestOwnership();
            this.realtimeView.RequestOwnership();
            return this;
        }

        return null;
    }

    public void Release()
    {
        Debug.Log("Released: ");
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