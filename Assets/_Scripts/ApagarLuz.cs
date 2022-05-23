using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApagarLuz : MonoBehaviour
{
    public GameObject luces;
    private float tiempo = 66f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tiempo -= Time.deltaTime;

        if (tiempo <= 0)
        {
            luces.SetActive(false);
        }

    }
}
