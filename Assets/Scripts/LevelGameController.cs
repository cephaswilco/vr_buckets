using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGameController : MonoBehaviour
{

    [SerializeField]
    BasketballGameController basketballGameController;

    [SerializeField]
    GorillaBallGameController gorillaBallGameController;

    [SerializeField]
    MultiplayerScore multiplayerScore;


    private void Awake()
    {
        
    }

    public void InitializeBasketballGame()
    {
        multiplayerScore.ResetScore();
       /* basketballGameController.gameObject.SetActive(true);
        gorillaBallGameController.gameObject.SetActive(false);*/
    }
    public void InitializeGorillaballGame()
    {
        multiplayerScore.ResetScore();
       /* basketballGameController.gameObject.SetActive(false);
        gorillaBallGameController.gameObject.SetActive(true);*/

    }

}

public interface GameTrigger
{
    public void Trigger();

}