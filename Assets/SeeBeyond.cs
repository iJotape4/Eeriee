using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SeeBeyond : MonoBehaviour
{
    public Camera _mainCamera;
    public Camera _seeBeyondCamera;
    public Light _seeBeyondLight;
    public PlayerInput _playerInput;
    // Start is called before the first frame update
    void Start()
    {
        _seeBeyondLight.enabled = false;
        _playerInput = FindObjectOfType<PlayerInput>();

    }

    // Update is called once per frame
    void Update()
    {
        _seeBeyondCamera.enabled = (_seeBeyondLight.enabled ?  true : false);
        _mainCamera.enabled = (_seeBeyondCamera.enabled ? false : true);
    }
}
