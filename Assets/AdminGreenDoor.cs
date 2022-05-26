using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminGreenDoor : PrisonDoor
{
    public override void Interact()
    {
        if (GameManager.Instance.EerieObtained)
        {
            base.Interact();
        }
        {
            _needKey.Play();
        }
    }
}
