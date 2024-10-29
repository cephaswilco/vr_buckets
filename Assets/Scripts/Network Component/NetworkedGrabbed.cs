using UnityEngine;
using Normal.Realtime;

public class NetworkedGrabbed : RealtimeComponent<GrabbedModel>
{
    public int playerID
    {
        get => model._playerID;
        set => model._playerID = value;
    }

    protected override void OnRealtimeModelReplaced(GrabbedModel previousModel, GrabbedModel currentModel)
    {
        if (previousModel != null)
        {
            // Unsubscribe from events on the old model
            previousModel.playerIDDidChange -= PlayerIDDidChange;
        }

        if (currentModel != null)
        {
            // Update the UI or state with the current model's data
            UpdatePlayerIDDisplay(currentModel._playerID);

            // Subscribe to events on the new model
            currentModel.playerIDDidChange += PlayerIDDidChange;
        }
    }

    private void PlayerIDDidChange(GrabbedModel model, int playerID)
    {
        UpdatePlayerIDDisplay(playerID);
    }

    private void UpdatePlayerIDDisplay(int playerID)
    {

    }

    public void SetPlayerID(int newPlayerID)
    {
        if (realtime != null && realtimeView.isOwnedLocallySelf)
        {
            model._playerID = newPlayerID;
        }
    }
}