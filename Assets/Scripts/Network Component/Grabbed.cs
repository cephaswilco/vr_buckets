using UnityEngine;
using Normal.Realtime;

public class Grabbed : RealtimeComponent<GrabbedModel>
{

    public int currentPlayerID;

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
            UpdatePlayerIDDisplay(currentModel.playerID);

            // Subscribe to events on the new model
            currentModel.playerIDDidChange += PlayerIDDidChange;
        }
    }

    private void PlayerIDDidChange(GrabbedModel model, int playerID)
    {
        Debug.Log("PLAYER DID CHANGE: " + playerID);
        UpdatePlayerIDDisplay(playerID);
    }

    private void UpdatePlayerIDDisplay(int playerID)
    {
        currentPlayerID = playerID;
    }

    public int GetPlayerID()
    {
        return model.playerID;
    }

    public void SetPlayerID(int newPlayerID)
    {
         Debug.Log("Set Player " + newPlayerID);
         model.playerID = newPlayerID;  
    }
}