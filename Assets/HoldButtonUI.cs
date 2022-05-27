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
        _playerInput = FindObjectOfType<PlayerInput>();
        _holdbutton.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerInput == null)
        {
            try
            {
                _playerInput = FindObjectOfType<PlayerInput>();
            }
            catch { }

        }

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
