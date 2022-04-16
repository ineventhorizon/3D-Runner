using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalPanel : UIBase
{
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private TextMeshProUGUI percentageText;

    public void UpdatePaintProgress(int value)
    {
        progressBar.SetCurrentFill(value);
        percentageText.SetText($"%{value}");
    }
    public override void DisablePanel()
    {
        base.DisablePanel();
        percentageText.SetText("Fill Amount");
        progressBar.ResetProgressBar();
    }
}
