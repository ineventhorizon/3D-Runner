using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanel : UIBase
{
    
    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }
}
