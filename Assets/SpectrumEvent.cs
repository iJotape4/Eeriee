using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            Instantiate(Resources.Load<GameObject>("Prefabs/SpectrumParent"), transform.position, transform.rotation);
        }

    }
}
