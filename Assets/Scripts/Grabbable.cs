using Normal.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grabbable : MonoBehaviour, IGrabbable  
{

    RealtimeTransform realtimeTransform;

    void Awake()
    {
        realtimeTransform = GetComponent<RealtimeTransform>();
    }

    void Update()
    {
        
    }

    public Grabbable Grab(Transform grabberTransform)
    {
        if (!realtimeTransform.isOwnedLocallySelf || realtimeTransform.isOwnedRemotelySelf)
        {  
            realtimeTransform.RequestOwnership();         
        }
        return this;
    }

    public void Release()
    {
        // Optionally, you can relinquish ownership when done
        realtimeTransform.ClearOwnership();
    }

}


public interface IGrabbable
{
    public Grabbable Grab(Transform transform);
}