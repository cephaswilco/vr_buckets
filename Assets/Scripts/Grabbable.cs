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
        if (!realtimeTransform.isOwnedRemotelySelf)
        {  
            realtimeTransform.RequestOwnership();
            return this;
        }

        return null;
    }

    public void Release()
    {
        if (realtimeTransform.isOwnedLocallySelf)
        {
            realtimeTransform.ClearOwnership();
        }
    }

}


public interface IGrabbable
{
    public Grabbable Grab(Transform transform);
}