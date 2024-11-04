using Normal.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTriggerHandler : MonoBehaviour
{
    [SerializeField]
    GameController gameController;

    float time = 0f;

    private void Update()
    {
        time += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (time < 5f)
            return;

        if (!other.GetComponent<RealtimeTransform>().isOwnedLocallySelf)
            return;

        if (other.gameObject.layer == LayerMask.NameToLayer("Grabbable")) 
        { 
            gameController.InitializeGame();
        }
    }
}
