using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ApagarLuz : MonoBehaviour
{
    public GameObject luces;
    public PlayerInput _playerInput;
    private float tiempo = 66f;
    public InteractableObject _eventConversation;
    // Start is called before the first frame update
    void Start()
    {
        _playerInput = FindObjectOfType<PlayerInput>();
        _playerInput.SwitchCurrentActionMap("LimitedPlayer");
        _eventConversation = GetComponent<InteractableObject>();
    }

    // Update is called once per frame
    void Update()
    {
        tiempo -= Time.deltaTime;

        if (tiempo <= 0)
        {
            luces.SetActive(false);
            _eventConversation.activate();
        }

    }
}
