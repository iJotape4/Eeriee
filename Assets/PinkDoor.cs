using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkDoor : PrisonDoor
{
    public override void Interact()
    {
        if (GameManager.Instance.PinkCard)
        {
            base.Interact();
        }
    }
}
