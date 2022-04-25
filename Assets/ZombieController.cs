using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Collider))]
[RequireComponent (typeof (Animator))]

public class ZombieController : MonoBehaviour
{
    private Rigidbody _rb;
    private Animator _anim;

    private UIManager uimanager;

    // Start is called before the first frame update
    void Start()
    {
        uimanager = UIManager.Instance;
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Patrol()
    {

    }

    void PlayerDetection()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            uimanager.UpdateHealth(10);
        }
    }
}
