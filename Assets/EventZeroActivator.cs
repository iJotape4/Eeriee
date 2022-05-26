using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventZeroActivator : MonoBehaviour
{
    void Start()
    {
        GetComponent<BoxCollider>().enabled = true;
    }
}
