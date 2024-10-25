using Normal.Realtime;
using Normal.Realtime.Serialization;
using System;
using System.Diagnostics;

[RealtimeModel]
public partial class PlayerScoreModel
{
    [RealtimeProperty(1, true, true)]
    public int _playerID; // Unique identifier for the player

    [RealtimeProperty(2, true, true)]
    public int _score; // Player's score

}