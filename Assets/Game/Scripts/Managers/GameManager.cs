using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField, ReadOnly] public GameState CurrentGameState = GameState.MENU;

    public void StartGame()
    {
        UIManager.Instance.StartPanel.DisablePanel();
        Observer.CharactersAnimState?.Invoke(CharacterAnimState.RUNNING);
        UIManager.Instance.InGamePanel.EnablePanel();
        CurrentGameState = GameState.GAMEPLAY;
    }

    public void StartFinal()
    {
        CurrentGameState = GameState.FINAL;
        Observer.CharactersAnimState?.Invoke(CharacterAnimState.IDLE);
        UIManager.Instance.InGamePanel.DisablePanel();
        UIManager.Instance.FinalPanel.EnablePanel();
        CameraManager.Instance.SwitchCam("FinalCam");
    }
    public void RestartGame()
    {
        CurrentGameState = GameState.MENU;
        //UIManager.Instance.FailScreen.DisablePanel();
        UIManager.Instance.InGamePanel.DisablePanel();
        UIManager.Instance.StartPanel.EnablePanel();
        MySceneManager.Instance.RestartActiveScene();
    }
    public void NextLevel()
    {
        CurrentGameState = GameState.MENU;
        UIManager.Instance.FinalPanel.DisablePanel();
        UIManager.Instance.WinPanel.DisablePanel();
        UIManager.Instance.StartPanel.EnablePanel();
        MySceneManager.Instance.LoadNextLevel();
    }
}

public enum GameState
{
    GAMEPLAY = 0,
    MENU = 1,
    PAUSED = 2,
    FINAL = 3
}
