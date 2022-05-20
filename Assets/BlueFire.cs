using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFire : MonoBehaviour
{
    public Rigidbody _blueFireRB;
    public ParticleSystem _blueFire;
    public ParticleSystem _xplotion;

    [SerializeField] private float shotForce = 2000f;

    public void Start()
    {
        _blueFireRB = GetComponent<Rigidbody>();
        _blueFire = GetComponent<ParticleSystem>();
        _xplotion = transform.GetChild(0).GetComponent< ParticleSystem>();
        _xplotion.Stop();
    }

    public void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(VFXChanges());
    }

    public IEnumerator VFXChanges()
    {
        _blueFire.Stop();
        _xplotion.Play();
        yield return new WaitForSeconds(1f);
        _xplotion.Stop();
    }

}
