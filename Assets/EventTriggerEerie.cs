using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerEerie : MonoBehaviour
{

    public  InteractableObject _texts;
    // Start is called before the first frame update
    void Start()
    {
        _texts = GetComponent<InteractableObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _texts.activate();
            GetComponent<BoxCollider>().enabled = false;
        }
      
    }
}
