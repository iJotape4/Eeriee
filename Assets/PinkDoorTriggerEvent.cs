using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PinkDoorTriggerEvent : MonoBehaviour
{

    public MeshRenderer[] _mrs;
    public BoxCollider _this;
    public PlayerInput _pi;
    public InteractableObject _io;
    void Start()
    {
        _mrs = GetComponentsInChildren<MeshRenderer>();
        SwitchmrActivation();
        _this = GetComponent<BoxCollider>();
        _this.enabled = true;
        _pi = FindObjectOfType<PlayerInput>();
        _io = GetComponent<InteractableObject>();
    }


    void SwitchmrActivation()
    {
        foreach (MeshRenderer mr in _mrs)
        {
            mr.enabled = (mr.enabled ? false: true) ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.PinkCard)
        {
            this.gameObject.SetActive(false);
        }     
    }

    public IEnumerator playerRecovery()
    {
        while (!_io._finishedEvent)
        {
            yield return new WaitForEndOfFrame();
        }
      _pi.SwitchCurrentActionMap("Player");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SwitchmrActivation();
            _pi.SwitchCurrentActionMap("Dialogues");
            StartCoroutine(playerRecovery());
        }

    }  
}
