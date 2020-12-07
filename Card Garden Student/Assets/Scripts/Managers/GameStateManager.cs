using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu,
    GamePause,
    EncounterPause,
    MidEncounter,
    WaitingForNextEncounter,
    CompletedAllEncounters
}

public class GameStateManager : SingletonPattern<GameStateManager>
{
    public GameState CurrentState;
    public float gameTimer = 0.0f;


    private void Update()
    {
        CheckAction();

        //if (gameTimer < 5f)
        //{
        //    if (gameTimer > 2f)
        //    {
        //        TutorialUI.Instance.LoadTutorial();
        //        TutorialUI.Instance.ShowPopUp();
        //    }
        //}
    }

    private void CheckAction()
    {
        switch (CurrentState)
        {
            case GameState.MainMenu:
                break;
            case GameState.GamePause:
                break;
            case GameState.EncounterPause:
                break;
            case GameState.MidEncounter:
                AddTime();
                break;
            case GameState.WaitingForNextEncounter:
                break;
            case GameState.CompletedAllEncounters:
                break;
            default:
                break;
        }
    }

    private void AddTime()
    {
        gameTimer += Time.deltaTime;
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
    }

}
