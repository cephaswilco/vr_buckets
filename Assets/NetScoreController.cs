using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetScoreController : MonoBehaviour
{

    [SerializeField]
    ScoreTrigger topScoreTrigger;

    [SerializeField]
    ScoreTrigger bottomScoreTrigger;

    [SerializeField]
    ScoreReporter scoreReporter;

    float resetTime = 0f;
    float resetAfter = 5f;

    bool inTopTrigger = false;
    bool inBottomTrigger = false;

    float scoreBlockerTime = 0f;

    float scoreBlockerLimit = 3f;

    private void Update()
    {
        scoreBlockerTime += Time.deltaTime;
    }


    private void Awake()
    {
        

        topScoreTrigger.ReportTriggerEntered += ReportTopEnter;
        topScoreTrigger.ReportTriggerExit += ReportTopExit;


        bottomScoreTrigger.ReportTriggerEntered += ReportBottomEnter;
        bottomScoreTrigger.ReportTriggerExit += ReportBottomExit;
    }

    // This is just a basica implementation, for a more fair and rigorous implementation you would need to keep track of the direction of the ball as it passes through the triggers to ensure a regulation basket score

    public void ReportBottomEnter(NetworkedBall networkedBall)
    {

    }

    public void ReportBottomExit(NetworkedBall networkedBall)
    {
        if (scoreBlockerTime > scoreBlockerLimit)
        {
            scoreBlockerTime = 0f;
            Scored(networkedBall);
        }
    }

    public void ReportTopEnter(NetworkedBall networkedBall)
    {

    }

    public void ReportTopExit(NetworkedBall networkedBall)
    {

    }


    public void Scored(NetworkedBall networkedBall)
    {
        // 
        scoreReporter.ReportScore(networkedBall.GetCurrentPlayerID(), +1);
    }



}
