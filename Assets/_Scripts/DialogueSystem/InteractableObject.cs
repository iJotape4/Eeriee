 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))] 
public class InteractableObject : MonoBehaviour
{

    public static BoxCollider[] _eventsList;
    public static GameObject _crossMark;
    public bool _finishedEvent;
    public bool _isMainEvent;
    public TextsDictionary _texts;


    private void Awake()
    {
        _crossMark = GameObject.Find("CrossMark");
        _eventsList = transform.parent.GetComponentsInChildren<BoxCollider>();
        GetComponent<BoxCollider>().isTrigger = true;
        if(_isMainEvent)
        GetComponent<BoxCollider>().enabled = false;
    }

    private void Update()
    {
        if (_finishedEvent)
        {
            _finishedEvent = false;

            if(_isMainEvent)
            GameManager.Instance.NextEvent();
        }
    }

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
