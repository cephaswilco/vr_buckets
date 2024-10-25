using Normal.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealtimeCamera : MonoBehaviour
{
    [SerializeField]
    RealtimeView realtimeView;

    CameraSetter cameraSetter;
    ScoreReporter scoreReporter;

    // Start is called before the first frame update
    void Start()
    {
        cameraSetter = FindAnyObjectByType<CameraSetter>();
        scoreReporter = FindAnyObjectByType<ScoreReporter>();

        if (realtimeView.isOwnedLocallySelf)
        {
            cameraSetter.SetCanvasCamera(transform.GetComponent<Camera>());
            transform.GetComponent<Camera>().enabled = true;

        }
     
    }

    // Update is called once per frame
    void Update()
    {
        if (realtimeView.isOwnedLocallySelf)
        {
            // This is to test input
            if (Input.GetKeyDown(KeyCode.K))
            {
                //scoreReporter.ReportScore(realtimeView.ownerIDSelf, 1);
            }
        }
    }
}
