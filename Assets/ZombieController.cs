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

    // Start is called before the first frame update
    void Start()
    {
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
}
