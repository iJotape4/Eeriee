using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterJetController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
