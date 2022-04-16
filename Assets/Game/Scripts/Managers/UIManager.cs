using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] public InGamePanel InGamePanel;
    [SerializeField] public StartPanel StartPanel;
    [SerializeField] public FailPanel FailPanel;
    [SerializeField] public WinPanel WinPanel;
    [SerializeField] public FinalPanel FinalPanel;
}
