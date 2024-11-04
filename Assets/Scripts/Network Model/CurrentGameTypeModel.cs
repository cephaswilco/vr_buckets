using Normal.Realtime;
using Normal.Realtime.Serialization;
using System;
using System.Diagnostics;

[RealtimeModel]
public partial class CurrentGameTypeModel
{
    [RealtimeProperty(1, true, true)]
    private int _currentGameType = 0; // Unique identifier for the player
}