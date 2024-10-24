using Normal.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour, IGrabbable  
{

    RealtimeTransform realtimeTransform;

    void Awake()
    {
        realtimeTransform = GetComponent<RealtimeTransform>();
    }

    public Transform Grab(Transform grabberTransform)
    {
        if (!realtimeTransform.isOwnedLocallySelf || realtimeTransform.isOwnedRemotelySelf)
        {
            realtimeTransform.RequestOwnership();
        }
        return this.transform;
    }

    public void Release()
    {
        // Optionally, you can relinquish ownership when done
        realtimeTransform.ClearOwnership();
    }

}


public interface IGrabbable
{
    public Transform Grab(Transform transform);
}