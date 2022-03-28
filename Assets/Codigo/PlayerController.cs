using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    //Camera
    private Camera mainCamera;

    [Header("Physics")]
    public Rigidbody playerRigidbody;

    [Header("Animation")]
    public Animator playerAnimator;
    private int playerMovementID;
    private int playerAttackID;

    [Header("Input")]
    private string actionMapGameplay = "Player Controls";
    private string actionMapMenu = "Menu Controls";
    
    
    private Vector2 movementInput;
    private bool currentInput = false;

    [Header("Movement Settings")]
    public float movementSpeed = 3;
    public float smoothingSpeed = 1;
    private Vector3 currentDirection;
    private Vector3 rawDirection;
    private Vector3 smoothDirection;
    private Vector3 movement;
    #endregion

    private Vector3 inputVector;

    void CalcularInputDeMovimiento()
    {
        var inputVertical = Input.GetAxisRaw("Vertical");
        var inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVector = new Vector3(inputHorizontal, 0, inputVertical);
    }

    void CalcularAtaque()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Atacar();
        }
    }

    #region Eventos De Unity
    void Start()
    {
        FindCamera();
        SetupAnimationIDs();
    }

    void FindCamera()
    {
        mainCamera = Camera.main;
    }

    void SetupAnimationIDs()
    {
        playerMovementID = Animator.StringToHash("Movement");
        playerAttackID = Animator.StringToHash("Attack");
    }

    void Update()
    {
        CalcularInputDeMovimiento();
        CalcularAtaque();

        if (inputVector == Vector3.zero)
        {
            currentInput = false;
        }
        else if (inputVector != Vector3.zero)
        {           
            currentInput = true;
        }
    }

    void FixedUpdate()
    {
        CalculateDesiredDirection();
        ConvertDirectionFromRawToSmooth();
        MoveThePlayer();
        AnimatePlayerMovement();
        TurnThePlayer();
    }

    #endregion

    #region Movimiento Del Jugador
    void Atacar()
    {
        playerAnimator.SetTrigger(playerAttackID);
    }

    void CalculateDesiredDirection()
    {
        //Camera Direction
		var cameraForward = mainCamera.transform.forward;
		var cameraRight = mainCamera.transform.right;

		cameraForward.y = 0f;
		cameraRight.y = 0f;

        rawDirection = cameraForward * inputVector.z + cameraRight * inputVector.x;
    }

    void ConvertDirectionFromRawToSmooth()
    {   
        if(currentInput == true)
        {
            smoothDirection = Vector3.Lerp(smoothDirection, rawDirection, Time.deltaTime * smoothingSpeed);
        } else if(currentInput == false)
        {
            smoothDirection = Vector3.zero;
        }
        
    }

    void MoveThePlayer()
    {
        if(currentInput == true)
        {
            movement.Set(smoothDirection.x, 0f, smoothDirection.z);
            movement = movement.normalized * movementSpeed * Time.deltaTime;
            playerRigidbody.MovePosition(transform.position + movement);
        }

    }

    void TurnThePlayer()
    {
        if(currentInput == true)
        {
            Quaternion newRotation = Quaternion.LookRotation(smoothDirection);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void AnimatePlayerMovement()
    {
        playerAnimator.SetFloat(playerMovementID, inputVector.sqrMagnitude);
    }

    #endregion

}
