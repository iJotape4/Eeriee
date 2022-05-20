using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class HoldButtonUI : MonoBehaviour
{

    public Image _holdbutton;
    public PlayerInput _playerInput;
    // Start is called before the first frame update
    void Start()
    {
        _holdbutton = GetComponent<Image>();
        _playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        _holdbutton.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        fullfill();
    }

    void fullfill()
    {
        InputAction _hold = _playerInput.actions["Next"];
        if (_hold.IsPressed())
        {
            _holdbutton.fillAmount += 1f/130f ;
        }
        if (_hold.WasReleasedThisFrame())
            _holdbutton.fillAmount = 0f;
        
    }
}
