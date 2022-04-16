using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanel : UIBase
{
    
    public void StartGame()
    {
        Debug.Log("Button pressed, Starting game");
        GameManager.Instance.StartGame();
    }
}
