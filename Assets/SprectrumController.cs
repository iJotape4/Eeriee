using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprectrumController : MonoBehaviour
{
    public Transform _playerTarget;
    public ParticleSystem _blueFire;
    public ParticleSystem _xplosion;


    // Start is called before the first frame update
    void Start()
    {
        _playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _blueFire = GetComponentInChildren<ParticleSystem>();
        _xplosion = _blueFire.gameObject.GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
