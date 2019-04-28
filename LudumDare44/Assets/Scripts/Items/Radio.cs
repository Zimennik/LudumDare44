using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : InteractableItem
{
    private List<string> Info = new List<string>();

    public void Awake()
    {
        IsBroken = true;
        Info.Add("Day first. Azaza. ......... .. . .");
        Info.Add("");
        Info.Add("");
        Info.Add("");
    }

    public override void OnInteractFinished()
    {
        string info = "";
        if (GameManager.Instance.CurrentDay >= 0 && GameManager.Instance.CurrentDay <= Info.Count + 1)
        {
            info = Info[GameManager.Instance.CurrentDay];
            MessageBox.Instance.ShowText(info, 15f);
        }
    }
}