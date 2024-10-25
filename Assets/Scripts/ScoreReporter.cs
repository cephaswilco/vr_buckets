using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreReporter : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI scoreBoard;

    MultiplayerScore _playerScore;


    private void Awake()
    {
        _playerScore = GetComponent<MultiplayerScore>();
        _playerScore.OnScoreChanged += ScoreChanged;
    }


    public void ScoreChanged() 
    {
        UpdateScoreBoard();
    }


    public void ReportScore(int player, int scoreToAdd)
    {
        _playerScore.AddOrUpdatePlayerScore((uint)player, _playerScore.GetPlayerScore(player) + scoreToAdd);
        UpdateScoreBoard();
    }

    void UpdateScoreBoard()
    {
        string scoreBoardText = "";
        foreach (int item in _playerScore.GetAllPlayerIDs())
        {
            scoreBoardText += "Player " + (item + 1) + " : " + _playerScore.GetPlayerScore(item);
            scoreBoardText += "\n";
        }

        scoreBoard.text = scoreBoardText;
    }
}
