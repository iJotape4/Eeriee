using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonDoor : Interactable
{

    private Animator _anim;
    public AudioSource _needKey;
    private string _opened = "Opened";
    private float _timeOpened = 2f;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _needKey = GetComponent<AudioSource>();
    }

     public override void Interact()
    {
        base.Interact();
        _anim.SetBool(_opened, _anim.GetBool(_opened) ? false : true);
        StartCoroutine(doorTemp());      
    }

    public IEnumerator doorTemp()
    {
        gameObject.layer = (gameObject.layer == LayerMask.NameToLayer("Player") ? LayerMask.NameToLayer("Interactable") : LayerMask.NameToLayer("Player"));
        yield return new WaitForSeconds(_timeOpened);
        gameObject.layer = (gameObject.layer == LayerMask.NameToLayer("Player") ? LayerMask.NameToLayer("Interactable") : LayerMask.NameToLayer("Player"));
        _anim.SetBool(_opened, _anim.GetBool(_opened) ? false : true);
    }
}
