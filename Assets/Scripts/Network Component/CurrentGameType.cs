using UnityEngine;
using Normal.Realtime;

public class CurrentGameType : RealtimeComponent<CurrentGameTypeModel>
{

    public int gameTypeID;

    [SerializeField]
    LevelGameController levelGameController;

    protected override void OnRealtimeModelReplaced(CurrentGameTypeModel previousModel, CurrentGameTypeModel currentModel)
    {
        if (previousModel != null)
        {
            // Unsubscribe from events on the old model
            previousModel.currentGameTypeDidChange -= GameTypeDidChange;
        }

        if (currentModel != null)
        {
            // Update the UI or state with the current model's data
            UpdateGameTypeIDDisplay(currentModel.currentGameType);

            // Subscribe to events on the new model
            currentModel.currentGameTypeDidChange += GameTypeDidChange;
        }
    }

    private void GameTypeDidChange(CurrentGameTypeModel model, int gameTypeID)
    {
        Debug.Log("Game Type DID CHANGE: " + gameTypeID);
        UpdateGameTypeIDDisplay(gameTypeID);
    }

    private void UpdateGameTypeIDDisplay(int gameTypeID)
    {
        this.gameTypeID = gameTypeID;

        if (gameTypeID == 0) 
        {
            levelGameController.InitializeBasketballGame();
        } else if (gameTypeID == 1)
        {
            levelGameController.InitializeGorillaballGame();
        }
    }

    public int GetGameTypeID()
    {
        return model.currentGameType;
    }

    public void SetGameTypeID(int gameTypeID)
    {
         Debug.Log("Set GameType " + gameTypeID);
         model.currentGameType = gameTypeID;  
    }
}