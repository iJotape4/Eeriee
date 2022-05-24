using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraInteraction : MonoBehaviour
{
    private  Transform _camera;
    public float _rayDistance =3f;
    private PlayerInput _playerInput;
    private InputAction _interact;
    void Start()
    {
        _camera = this.transform;
        _playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        _interact = _playerInput.actions["Interact"];
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(_camera.position, _camera.forward * _rayDistance, Color.red); 

        if (_interact.WasPressedThisFrame()){
            RaycastHit hit;
            if (Physics.Raycast(_camera.position, _camera.forward, out hit, _rayDistance, LayerMask.GetMask("Interactable"))){
                hit.transform.GetComponent<Interactable>().Interact();
           }

        }
    }
}
