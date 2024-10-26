using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NetScoreController : MonoBehaviour
{

    [SerializeField]
    ScoreTrigger topScoreTrigger;

    [SerializeField]
    ScoreTrigger bottomScoreTrigger;

    [SerializeField]
    ScoreReporter scoreReporter;

    [SerializeField]
    TextMeshProUGUI textMeshProUGUI;

    float scoreAttemptTime = 0f;
    float scoreAttemptLimit = 3f;

    float scoreBlockerTime = 0f;
    float scoreBlockerLimit = 5f;  // This should represent enough time to block goals that have been attempted via throwing the ball underneathe the hoop and back in.

    [SerializeField]
    int scoreWorth = 1;


    private void Update()
    {
        scoreBlockerTime += Time.deltaTime;
        scoreAttemptTime += Time.deltaTime;


        Debug.Log("------------");
        Debug.Log("scoreBlockerTime: " + scoreBlockerTime);
        Debug.Log("scoreAttemptTime: " + scoreAttemptTime);
    }


    private void Awake()
    {

        scoreBlockerTime = scoreBlockerLimit;

        textMeshProUGUI.text = scoreWorth+"";

        topScoreTrigger.ReportTriggerEntered += ReportTopEnter;
        topScoreTrigger.ReportTriggerExit += ReportTopExit;


        bottomScoreTrigger.ReportTriggerEntered += ReportBottomEnter;
        bottomScoreTrigger.ReportTriggerExit += ReportBottomExit;
    }



    public void ReportTopEnter(NetworkedBall networkedBall)
    {
        if (scoreBlockerTime > scoreBlockerLimit)
        {
            scoreAttemptTime = 0f;
        }
    }

    public void ReportTopExit(NetworkedBall networkedBall)
    {
        if (scoreBlockerTime > scoreBlockerLimit)
        {
            scoreAttemptTime = 0f;
        }
    }


    public void ReportBottomEnter(NetworkedBall networkedBall)
    {
        // If ball is coming through the hoop, extend the limit to allow a goal but a few seconds, otherwise if it enters bottom without coming through top (as counted by the scoreAttemptTime), reset the scoreBlockerTime so they can't reverse basket.
        if (scoreAttemptTime < scoreAttemptLimit)
        {
            if (scoreBlockerTime > scoreBlockerLimit)
            {
                 scoreAttemptTime = -2f;
            }     
        } 
        else
        {
            scoreBlockerTime = 0;
        }
    }

    public void ReportBottomExit(NetworkedBall networkedBall)
    {
        // if scoreBlockerTime is greater than limit, it's not blocking
        if (scoreBlockerTime > scoreBlockerLimit)
        {
            // if scoreAttemptTime is less than AttemptLimit, it counts as a goal if it exits bottom
            if (scoreAttemptTime < scoreAttemptLimit)
            {
                scoreAttemptTime = scoreAttemptLimit;
                scoreBlockerTime = scoreBlockerLimit;
                Scored(networkedBall);
            } else // If scoreAttemptTime is out of range, set blocking time.
            {
                scoreBlockerTime = 0f;
            }
        }      
    }

    public void Scored(NetworkedBall networkedBall)
    {
        // 
        scoreReporter.ReportScore(networkedBall.GetCurrentPlayerID(), scoreWorth);
    }



}
