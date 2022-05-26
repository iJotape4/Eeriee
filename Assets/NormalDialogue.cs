using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDialogue : MonoBehaviour
{
    public bool _finishedEvent;
    public TextsDictionary _texts;

    public void activate()
    {
        FindObjectOfType<DialogueController>().ActivateDialogue(_texts, this.gameObject);

    }
}
