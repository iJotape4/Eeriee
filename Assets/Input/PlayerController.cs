using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]

public class PlayerController : MonoBehaviour
{
    #region Managers 
    [Header("Important Systems")]
    private UIManager uimanager;
    private GameManager gameManager;
    #endregion

    #region Player Stats
    [Header("Player")] //Movement
    [Tooltip("Move speed of the character in m/s")]
    public float MoveSpeed = 20.0f;
    [Tooltip("Sprint speed of the character in m/s")]
    public float SprintSpeed = 30.0f;
    [Tooltip("Rotation speed of the character")]
    public float RotationSpeed = 0.5f;
    [Tooltip("Acceleration and deceleration")]
    public float SpeedChangeRate = 10.0f;

    private float _speed;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;

    [Space(10)]  //Jump
    [Tooltip("The height the player can jump")]
    public float JumpHeight = 1.2f;
    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
    public float Gravity = -15.0f;

    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    [Space(10)] //Fall
    [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
    public float JumpTimeout = 0.1f;
    [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
    public float FallTimeout = 0.15f;

    [Header("Player Grounded")] //Ground Dettection
    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    public bool Grounded = true;
    [Tooltip("Useful for rough ground")]
    public float GroundedOffset = -0.14f;
    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    public float GroundedRadius = 0.5f;
    [Tooltip("What layers the character uses as ground")]
    public LayerMask GroundLayers;
    #endregion
    
    #region CineMachine
    [Header("Cinemachine")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject CinemachineCameraTarget;
    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 90.0f;
    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -90.0f;

    private float _cinemachineTargetPitch;
    private const float _threshold = 0.01f;
    #endregion

    #region GameObject Components
    [Header("Inspector Properties")]
    private CapsuleCollider _collider;
    private PlayerInput _playerInput;
    private CharacterController _controller;
    private GameObject _mainCamera;
    private GameObject _arms;

    #endregion

    #region Actions
    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _jumpAction;
    private InputAction _sprintAction;     
    private InputAction _interacAction;
    private InputAction _closeGameAction;
    private InputAction _weapon1Action;
    private InputAction _weapon2Action;
    private InputAction _weapon3Action;
    private InputAction _useWeaponAction;
    private InputAction _pauseAction;
    #endregion

    #region InputValues
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;
    public bool changeWeapon;
    public bool fire;
    #endregion

    #region Weapons Array
    [Header("Weapons")]
    public int _currentWeaponIndex = 0;
    private float _previousWeaponIndex;
    public GameObject currentWeapon;
    public GameObject[] weapons;
    #endregion

    #region Animations
    [Header("Animation")]
    [SerializeField]  public Animator _anim;
    private string _animAttackTrigger = "Attack";
    private string _animWeaponInt = "CurrentWeapon";
    private string _animChangeWeaponTrigger = "WeaponChange";

    [Header("AnimationsDictionary")]
    private string _animationIdle = "Anim_Arms_Idle";
    private string _animationBibleHit = "Anim_Arms_BibleHit";
    private string _animationHolyWater = "Anim_Arms_HolyWater";
    #endregion

    private bool IsCurrentDeviceMouse
    {
        get
        {
        #if ENABLE_INPUT_SYSTEM 
            return _playerInput.currentControlScheme == "KeyboardMouse";
                #else
				return false;
                #endif
        }
    }

    private void Awake()
    {
        // get a reference to our main camera
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
        uimanager = UIManager.Instance;

        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
        _lookAction = _playerInput.actions["Look"];
        _jumpAction = _playerInput.actions["Jump"];
        _sprintAction = _playerInput.actions["Sprint"];
        _interacAction = _playerInput.actions["Interact"];
        _closeGameAction = _playerInput.actions["CloseGame"];
        _weapon1Action = _playerInput.actions["Weapon 1"];
        _weapon2Action = _playerInput.actions["Weapon 2"];
        _weapon3Action = _playerInput.actions["Weapon 3"];
        _useWeaponAction = _playerInput.actions["UseWeapon"];
        _pauseAction = _playerInput.actions["Pause"];

        //_lookAction.performed += CameraRotation;
    } 

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _collider = GetComponentInChildren<CapsuleCollider>();
        _playerInput = GetComponent<PlayerInput>();

        _arms = GameObject.FindGameObjectWithTag("Arms"); _arms.transform.SetParent(_mainCamera.transform);
        _anim = _arms.GetComponent<Animator>();

        weapons = GameObject.FindGameObjectsWithTag("Weapon");        
        foreach (GameObject w in weapons)
        {
            w.SetActive(false);
        }
        currentWeapon = weapons[0];
        currentWeapon.SetActive(true);
        // reset our timeouts on start

        _jumpTimeoutDelta = JumpTimeout;
        _fallTimeoutDelta = FallTimeout;
    }

    private void Update()
    {
        GroundedCheck();
        /* JumpAndGravity();        
         Move();
         ChangeWeapon();
         Fire();
         Pause();*/
    }

    private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }
    #region LookAround Input Methods
    public void LookAround(InputAction.CallbackContext context)
    {
        Vector2 _look = context.ReadValue<Vector2>();     

        // if there is an input
        if (_look.sqrMagnitude >= _threshold && !GameManager.Instance.Ispaused)
        {
            //Don't multiply mouse input by Time.deltaTime
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

            _cinemachineTargetPitch += _look.y * RotationSpeed * deltaTimeMultiplier;
            _rotationVelocity = _look.x * RotationSpeed * deltaTimeMultiplier;

            // clamp our pitch rotation
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);
           
            // Update Cinemachine camera target pitch
            CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);
           
            // rotate the player left and right
            transform.Rotate(Vector3.up * _rotationVelocity);
        }
    }
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
    #endregion

    private void FixedUpdate()
    {
        if(_moveAction.ReadValue<Vector2>() == Vector2.zero)
            return;
        Vector2 _moveVector = _moveAction.ReadValue<Vector2>();
        Move(_moveVector);
    }

    #region Move Input Methods
    public void Move(Vector2 _move)
    {
        bool _sprint = _sprintAction.IsPressed();
        // set target speed based on move speed, sprint speed and if sprint is pressed
        float targetSpeed = _sprint ? SprintSpeed : MoveSpeed;
        // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon
        // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is no input, set the target speed to 0
        if (_move == Vector2.zero) targetSpeed = 0.0f;
        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;
        float speedOffset = 0.1f;
        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * 1f, Time.deltaTime * SpeedChangeRate);
            // round speed to 3 decimal places
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }
        // normalise input direction
        Vector3 inputDirection = new Vector3(_move.x, 0.0f, _move.y).normalized;
        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (_move != Vector2.zero)
        {
            // move
            inputDirection = transform.right * _move.x + transform.forward * _move.y;
        }
        // move the player
        _controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        //Move()

    }



    #endregion

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.started) {             
        _anim.SetBool(_animAttackTrigger, true);
         Debug.Log("2");
        }
        

         /*   _anim.SetInteger(_animWeaponInt, _currentWeaponIndex);
            if (currentWeapon.name == "Bible")
                StartCoroutine(BibleHit());
            if (currentWeapon.name == "HolyWater")
                StartCoroutine(HolyWaterHit());*/
        
    }

}

