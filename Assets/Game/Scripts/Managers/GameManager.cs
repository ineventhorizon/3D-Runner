using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField, ReadOnly] public GameState CurrentGameState = GameState.MENU;

    public void StartGame()
    {
        UIManager.Instance.StartScreen.DisablePanel();
        Observer.OpponentsAnimState?.Invoke(CharacterAnimState.RUNNING);
        UIManager.Instance.InGameScreen.EnablePanel();
        CurrentGameState = GameState.GAMEPLAY;
    }

    public void StartFinal()
    {
        CurrentGameState = GameState.FINAL;
        Observer.OpponentsAnimState?.Invoke(CharacterAnimState.IDLE);
        //CameraManager.Instance.SwitchCam("FinalCam");
    }
}

public enum GameState
{
    GAMEPLAY = 0,
    MENU = 1,
    PAUSED = 2,
    FINAL = 3
}
