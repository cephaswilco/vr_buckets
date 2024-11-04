using Normal.Realtime;
using Normal.Realtime.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerScore : RealtimeComponent<MultiplayerScoreModel>
{
    private RealtimeDictionary<PlayerScoreModel> _playerScores;
    private int _modelChangedIndicator;
    public event Action OnScoreChanged;

    protected override void OnRealtimeModelReplaced(MultiplayerScoreModel previousModel, MultiplayerScoreModel currentModel)
    {
        if (previousModel != null)
        {
            // Unsubscribe from the previous model's events
            previousModel.playerScores.modelAdded -= OnPlayerScoreAdded;
            previousModel.playerScores.modelRemoved -= OnPlayerScoreRemoved;
            previousModel.modelChangedIndicatorDidChange -= DidScoreChange;
        }
       
        if (currentModel != null)
        {
            _playerScores = currentModel.playerScores;

            // Subscribe to the current model's events
            _playerScores.modelAdded += OnPlayerScoreAdded;
            _playerScores.modelRemoved += OnPlayerScoreRemoved;
            currentModel.modelChangedIndicatorDidChange += DidScoreChange;
        }
    }


    private void DidScoreChange(MultiplayerScoreModel model, int id)
    {
        OnScoreChanged?.Invoke();
    }

    // Event handler for when a player score is added
    private void OnPlayerScoreAdded(RealtimeDictionary<PlayerScoreModel> dictionary, uint key, PlayerScoreModel model, bool remote)
    {
        Debug.Log($"Player {model.playerID} added with score {model.score}. Remote: {remote}");

        if (remote)
        {
            OnScoreChanged?.Invoke();
        }
    }

    // Event handler for when a player score is removed
    private void OnPlayerScoreRemoved(RealtimeDictionary<PlayerScoreModel> dictionary, uint key, PlayerScoreModel model, bool remote)
    {
        Debug.Log($"Player {model.playerID} removed. Remote: {remote}");
    }

    public void ResetScore()
    {
        foreach (int id in GetAllPlayerIDs())
        {
            AddOrUpdatePlayerScore((uint)id, 0);
        }
    }

    public void AddOrUpdatePlayerScore(uint playerID, int score)
    {
        PlayerScoreModel playerScoreEntry;

        if (!_playerScores.TryGetValue(playerID, out playerScoreEntry))
        {
            // Player doesn't exist, add a new entry
            playerScoreEntry = new PlayerScoreModel
            {
                playerID = (int)playerID,
                score = score
            };
            _playerScores.Add(playerID, playerScoreEntry);
        }
        else
        {
            // Player exists, update the score
            playerScoreEntry.score = score;
            _playerScores.Add(playerID, playerScoreEntry);
        }

        model.modelChangedIndicator++;
    }

    public int GetPlayerScore(int playerID)
    {
        _playerScores.TryGetValue((uint)playerID, out var playerScoreEntry);

        if (playerScoreEntry.playerID == playerID)
        {
            return playerScoreEntry.score;
        } else
        {
            return 0;
        }
    }

    public List<int> GetAllPlayerIDs()
    {
        List<int> playerIDs = new List<int>();

        if (_playerScores == null)
        {
            return new List<int>();
        }

        foreach (var item in _playerScores)
        {
            playerIDs.Add((int)item.Key);
        }

        return playerIDs;
    }
}
