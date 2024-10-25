using Normal.Realtime;
using Normal.Realtime.Serialization;
using System;
using static UnityEngine.Rendering.DebugUI;

[RealtimeModel]
public partial class MultiplayerScoreModel
{
    [RealtimeProperty(1, true, true)]
    public RealtimeDictionary<PlayerScoreModel> _playerScores; // Dictionary to hold player scores

    [RealtimeProperty(2, true, true)]
    public int _modelChangedIndicator; // Unique identifier for the call

}