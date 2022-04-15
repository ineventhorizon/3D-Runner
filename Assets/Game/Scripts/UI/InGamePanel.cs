using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGamePanel : UIBase
{
    [SerializeField] private TextMeshProUGUI rankText;

    public void UpdateRankText(int rank)
    {
        rankText.SetText(rank.ToString());
    }

}
