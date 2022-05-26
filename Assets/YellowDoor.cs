using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowDoor : PrisonDoor
{
    public override void Interact()
    {
        if (GameManager.Instance.YellowCard)
        {
            base.Interact();
        }
        {
            _needKey.Play();
        }
    }
}
