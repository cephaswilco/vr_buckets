using Normal.Realtime;
using Normal.Realtime.Serialization;
using System;
using System.Diagnostics;

[RealtimeModel]
public partial class GrabbedModel 
{
    [RealtimeProperty(1, true, true)]
    private int _playerID = -1; // Unique identifier for the player
}