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
    void Start()
    {
        _camera = this.transform;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        _playerInput = _player.GetComponent<PlayerInput>();
        _interact = _playerInput.actions["Interact"];
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(_camera.position, _camera.forward * _rayDistance, Color.red); 

        if (_interact.WasPressedThisFrame() && _player._currentWeaponIndex==0){
            RaycastHit hit;
            if (Physics.Raycast(_camera.position, _camera.forward, out hit, _rayDistance, LayerMask.GetMask("Interactable"))){
                hit.transform.GetComponent<Interactable>().Interact();
           }

        }
    }
}
