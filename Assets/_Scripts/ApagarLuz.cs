using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ApagarLuz : MonoBehaviour
{
    public GameObject luces;
    public PlayerInput _playerInput;
    private float tiempo = 66f;
    public NormalDialogue _eventConversation;
    public BoxCollider _knockoutTriggerEvent;
    public GameObject _exitDoor;
    // Start is called before the first frame update

    private void Awake()
    {
        _eventConversation = GetComponent<NormalDialogue>();
    }

    void Start()
    {
        _playerInput = FindObjectOfType<PlayerInput>();
        _playerInput.SwitchCurrentActionMap("LimitedPlayer");
       
        _knockoutTriggerEvent = FindObjectOfType<KnockOutEvent>().GetComponentInChildren<BoxCollider>();
        _exitDoor = GameObject.Find("ExitDoor");
        _exitDoor.layer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerInput == null)
        {
            try
            {
                _playerInput = FindObjectOfType<PlayerInput>();
            }
            catch { }
           
        }

        tiempo -= Time.deltaTime;

        if (tiempo <= 0)
        {
            luces.SetActive(false);
            _eventConversation.activate();
            _knockoutTriggerEvent.enabled = true;
            _exitDoor.layer = 15;
        }
    }


    
}
