using UnityEngine;
using Normal.Realtime;

public class CurrentGameType : RealtimeComponent<CurrentGameTypeModel>
{

    public int gameTypeID;

    [SerializeField]
    LevelGameController levelGameController;

    [SerializeField]
    ScoreReporter reporter;

    float timeSinceAwake = 0;

    private void Awake() {

        timeSinceAwake += Time.deltaTime;
    }

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

        if (timeSinceAwake < 4)
        {
            return;
        }

        this.gameTypeID = gameTypeID;

        if (gameTypeID == 0) 
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