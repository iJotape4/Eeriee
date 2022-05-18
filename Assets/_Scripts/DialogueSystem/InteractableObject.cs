 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public TextsDictionary _texts;

    public void activate()
    {
        FindObjectOfType<DialogueController>().ActivateDialogue(_texts);
    }
}
