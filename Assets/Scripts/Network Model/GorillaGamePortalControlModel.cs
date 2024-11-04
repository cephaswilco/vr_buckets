using Normal.Realtime;
using Normal.Realtime.Serialization;
using System;
using System.Diagnostics;

[RealtimeModel]
public partial class GorillaGamePortalControlModel
{
    [RealtimeProperty(1, true, true)]
    public int _portal1 = -1; 

    [RealtimeProperty(2, true, true)]
    public int _portal2 = -1; 

    [RealtimeProperty(3, true, true)]
    public int _portal3 = -1;

}