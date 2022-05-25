using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartwachUIevent : MonoBehaviour
{
    private InteractableObject _io;
    void Start()
    {
        _io = GetComponent<InteractableObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_io._finishedEvent)
        {
            UIManager.Instance.SmartWatchUIconActivation();
        }
    }
}
