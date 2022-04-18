using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanel : UIBase
{
    public void NextLevel()
    {
        GameManager.Instance.NextLevel();
    }
}
