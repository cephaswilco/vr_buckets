using UnityEngine;
using Normal.Realtime;

public class OwnershipHandler : MonoBehaviour
{
    private RealtimeView _realtimeView;
    private RealtimeTransform _realtimeTransform;

    Rigidbody rigidBody;

    private void Awake()
    {
        _realtimeView = GetComponent<RealtimeView>();
        _realtimeTransform = GetComponent<RealtimeTransform>();
        rigidBody = GetComponent<Rigidbody>();  


        _realtimeTransform.ownerIDSelfDidChange += _realtimeTransform_ownerIDSelfDidChange;

    }

    private void _realtimeTransform_ownerIDSelfDidChange(RealtimeComponent<RealtimeTransformModel> model, int id)
    {

        Debug.Log("REALTIME TRANSFORM CHANGED");
   /*     if (!_realtimeTransform.isOwnedLocallySelf)
        {
            rigidBody.isKinematic = true;
        } else
        {
            rigidBody.isKinematic = false;
        }*/
    }

}