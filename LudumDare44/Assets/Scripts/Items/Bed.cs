﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : InteractableItem
{
    public override void OnInteractFinished()
    {
        GameManager.Instance.NextDay();
    }
}
