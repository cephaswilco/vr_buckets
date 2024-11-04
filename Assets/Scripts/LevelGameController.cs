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
    }

    public void InitializeGorillaballGame()
    {
        multiplayerScore.ResetScore();
    }

}

public interface GameTrigger
{
    public void Trigger();

}