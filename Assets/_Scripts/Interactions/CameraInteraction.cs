using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class CameraInteraction : MonoBehaviour
{
    private  Transform _camera;
    public float _rayDistance =3f;
    private PlayerInput _playerInput;
    private InputAction _interact;
    private FirstPersonController _player;
    private UIManager _uimanager;

    void Start()
    {
        _camera = this.transform;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        _playerInput = _player.GetComponent<PlayerInput>();
        _interact = _playerInput.actions["Interact"];
        _uimanager = UIManager.Instance;
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(_camera.position, _camera.forward * _rayDistance, Color.red);  
            RaycastHit hit;
            if (Physics.Raycast(_camera.position, _camera.forward, out hit, _rayDistance, LayerMask.GetMask("Interactable"))){
                if (_player._currentWeaponIndex == 0)
                {
                    _uimanager.ShowInteractionAllowed();
                    if(_interact.WasPressedThisFrame())
                    {
                        hit.transform.GetComponent<Interactable>().Interact();
                    }             
                }
                else
                {
                    _uimanager.ShowInteractionForbbidden();
                }
            }
            else
            {
            _uimanager.DisableInteraction();
            }
    }
}
