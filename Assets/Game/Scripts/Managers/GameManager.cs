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
        //UIManager.Instance.InGameScreen.EnablePanel();
        CurrentGameState = GameState.GAMEPLAY;
    }
}

public enum GameState
{
    GAMEPLAY = 0,
    MENU = 1,
    PAUSED = 2,
    FINAL = 3
}
