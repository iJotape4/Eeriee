using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EerieObtainEvent : MonoBehaviour
{
    private BoxCollider _eventTrigger;
    // Start is called before the first frame update
    void Start()
    {
        _eventTrigger = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_eventTrigger.enabled)
            GameManager.Instance.eerieObtention();
    }
}
