using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class FP_Player : MonoBehaviour
{

    #region Vars
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Rigidbody _rb;

    private Vector3 _inputVector;


    private float _jump_force;

    private bool currentInput = false;

    [Header("Movement Settings")]
    public float movementSpeed = 3;
    public float smoothingSpeed = 1;
    private CharacterController _controller;
    private float _verticalVelocity;

    private Vector3 currentDirection;
    private Vector3 rawDirection;
    private Vector3 smoothDirection;
    private Vector3 movement;


    #endregion

    #region Inputs
    private void OnMove(InputValue value)
    {
        Vector2 InputMovement = value.Get<Vector2>();
        _inputVector = new Vector3(InputMovement.x, 0, InputMovement.y);
        Debug.Log(_inputVector);
    }

    #endregion

    #region Player Movement
    void MoveThePlayer()
    {
        if (currentInput == true)
        {
            _rb.MovePosition(transform.position + _inputVector);
           // Debug.Log(movement);
        }

        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;
        //_controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

    }
    #endregion

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody>();
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
      
       // CalcularAtaque();

        if (_inputVector == Vector3.zero)
        {
            currentInput = false;
        }
        else if (_inputVector != Vector3.zero)
        {
            currentInput = true;
        }
    }

    private void FixedUpdate()
    {
        MoveThePlayer();
    }
}
