using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : InteractableItem
{
    private List<string> Info = new List<string>();

    public void Awake()
    {
        IsBroken = true;
        Info.Add("***static***");
        Info.Add("Poisonous mist of unknown origin has spread everywhere. DO NOT go out!");
        Info.Add("The data showed that a person can hold out no more than 20 seconds. But only at night! Be careful!");
        Info.Add("***static***");
        Info.Add("In addition to fog, it was very cold outside. If you hear this, be sure to heat your shelter!");
        Info.Add("***static***");
        Info.Add("We still do not know where this fog came from.");
        Info.Add("***statics***");
        Info.Add("We start the search operation of all the survivors! Be patient, we'll save you!");
        Info.Add("***static***");//10
        Info.Add("We believe it could be a terrorist attack ...");
        Info.Add("***static***");
        Info.Add("According to our data, 2% of the world's population survived.");
        Info.Add("***static***");
        Info.Add("Military help in finding survivors.");
        Info.Add("***static***");
        Info.Add("Scientists found out that these were aliens. That's how it is ...");
        Info.Add("***static***");
        Info.Add("Developer node: If you are reading this, then I have no time left for the game balance. Or maybe you are too cool for this game =)");
    }

    public override void OnInteractFinished()
    {
        string info = "";
        if (GameManager.Instance.CurrentDay >= 0 && GameManager.Instance.CurrentDay <= Info.Count)
        {
            info = Info[GameManager.Instance.CurrentDay];
            MessageBox.Instance.ShowText(info, 15f);
        }
    }
}