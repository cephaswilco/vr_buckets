using Normal.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealtimeCamera : MonoBehaviour
{
    [SerializeField]
    RealtimeView realtimeView;

    // Start is called before the first frame update
    void Start()
    {

        if (realtimeView.isOwnedLocallySelf)
        {
            transform.GetComponent<Camera>().enabled = true;
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
