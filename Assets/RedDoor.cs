using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDoor : PrisonDoor
{
   public override void Interact()
    {
        if (GameManager.Instance.CelldoorsOpened)
        {
            base.Interact();
        }
    }
}
