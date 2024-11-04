using UnityEngine;
using Normal.Realtime;

public class GorillaGamePortalControl: RealtimeComponent<GorillaGamePortalControlModel>
{

    public int currentPortal1;
    public int currentPortal2;
    public int currentPortal3;

    [SerializeField] 
    GorillaBallGameController gorillaBallGameController;

    protected override void OnRealtimeModelReplaced(GorillaGamePortalControlModel previousModel, GorillaGamePortalControlModel currentModel)
    {
        if (previousModel != null)
        {
            // Unsubscribe from events on the old model
            previousModel.portal1DidChange -= Portal1DidChange;
            previousModel.portal2DidChange -= Portal2DidChange;
            previousModel.portal3DidChange -= Portal3DidChange;
        }

        if (currentModel != null)
        {
            // Update the UI or state with the current model's data
            UpdatePortal1Display(currentModel.portal1);
            UpdatePortal2Display(currentModel.portal2);
            UpdatePortal3Display(currentModel.portal3);

            // Subscribe to events on the new model
            currentModel.portal1DidChange += Portal1DidChange;
            currentModel.portal2DidChange += Portal2DidChange;
            currentModel.portal3DidChange += Portal3DidChange;
        }
    }

    private void Portal1DidChange(GorillaGamePortalControlModel model, int portalID)
    {
        Debug.Log("Portal1DidChange: " + portalID);
        UpdatePortal1Display(portalID);
    }

    private void Portal2DidChange(GorillaGamePortalControlModel model, int portalID)
    {
        Debug.Log("Portal2DidChange: " + portalID);
        UpdatePortal2Display(portalID);
    }

    private void Portal3DidChange(GorillaGamePortalControlModel model, int portalID)
    {
        Debug.Log("Portal3DidChange: " + portalID);
        UpdatePortal3Display(portalID);
    }

    public void UpdatePortalStates()
    {
        //gorillaBallGameController.UpdateState();
    }

    private void UpdatePortal1Display(int portalID)
    {
        gorillaBallGameController.UpdatePortalState(currentPortal1, portalID);
        currentPortal1 = portalID;
    }

    private void UpdatePortal2Display(int portalID)
    {
        gorillaBallGameController.UpdatePortalState(currentPortal2, portalID);
        currentPortal2 = portalID;
    }

    private void UpdatePortal3Display(int portalID)
    {
        gorillaBallGameController.UpdatePortalState(currentPortal3, portalID);
        currentPortal3 = portalID;  
    }

    public int GetPortal1ID()
    {
        return model.portal1;
    }
    public int GetPortal2ID()
    {
        return model.portal2;
    }
       public int GetPortal3ID()
    {
        return model.portal3;
    }

    public void SetPortal1ID(int portalID)
    {
         Debug.Log("SetPortal1ID " + portalID);
         model.portal1 = portalID;  
    }
    public void SetPortal2ID(int portalID)
    {
        Debug.Log("SetPortal2ID " + portalID);
        model.portal2 = portalID;
    }
    public void SetPortal3ID(int portalID)
    {
        Debug.Log("SetPortal3ID " + portalID);
        model.portal3 = portalID;
    }

}