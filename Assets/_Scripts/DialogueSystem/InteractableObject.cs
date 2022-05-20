 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool _finishedEvent;
    public TextsDictionary _texts;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            activate();
            GetComponent<BoxCollider>().enabled = false;
        }

    }

    public void activate()
    {
        FindObjectOfType<DialogueController>().ActivateDialogue(_texts, this.gameObject);
       
    }
}
