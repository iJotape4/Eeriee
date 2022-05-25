using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerReactivable : MonoBehaviour
{
    public BoxCollider _bc;

    private void Start()
    {
        _bc = GetComponent<BoxCollider>();   
    }


    private void OnTriggerExit(Collider other)
    {
        _bc.enabled = true;
    }
}
