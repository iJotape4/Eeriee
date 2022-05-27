using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class KnockOutEvent : MonoBehaviour
{

    Animator _anim;
    public FirstPersonController _player;
    public PlayerInput playerInput;
    public NormalDialogue _io;
    //public Transform _lookAt;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _anim.enabled = false;

        playerInput = FindObjectOfType<PlayerInput>();
        _player = playerInput.GetComponent<FirstPersonController>();
        _io = GetComponentInParent<NormalDialogue>();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerInput.SwitchCurrentActionMap("Tutorial");
        _player.GetComponentInChildren<CapsuleCollider>().enabled = false;
        _player.GetComponentInChildren<Rigidbody>().useGravity = false;
        _anim.enabled = true;
        StartCoroutine(animKnockingout());
    }

    public IEnumerator animKnockingout()
    {
      //  _player.transform.LookAt(_lookAt);
        _player.transform.parent = _anim.gameObject.transform;       
        while (!_io._finishedEvent)
        {
            yield return new WaitForEndOfFrame();          
        }
        _player.transform.parent = null;
    }
}
