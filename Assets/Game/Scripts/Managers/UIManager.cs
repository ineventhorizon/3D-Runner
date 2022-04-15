using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] public InGamePanel InGameScreen;
    [SerializeField] public StartPanel StartScreen;
    [SerializeField] public FailPanel FailScreen;
    [SerializeField] public WinPanel WinScreen;
}
