﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : InteractableItem
{
   public override void OnInteractFinished()
   {
      GameManager.Instance.TeleportToSurface();
   }
}
