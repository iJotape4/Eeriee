using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class EerieController : MonoBehaviour
{
        
    [Header("Animation Parameters")]
    public Animator _anim;
    private string _animSeeBeyondBool = "SeeBeyond";
    private string _animWalkBool = "Walking";
    private string _animIdleBool = "Idle";
    private string _animRunningBol = "Run";
    private string _animStopTerrifyBool = "StopTerrify";
    private string _animSpeedFloat = "Speed";

    public float _speed =2f;
    private Light _seeBeyondLight;

    public FirstPersonController _player;
    public PlayerInput _playerInput;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _seeBeyondLight = GameObject.Find("SeeBeyondLigth").GetComponent<Light>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        _playerInput = _player.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        SeeBeyond();
        _anim.SetFloat(_animSpeedFloat, _speed);
        if (GameManager.Instance.EerieObtained)
        {
            FollowPlayer();
        }
    }
    public void SeeBeyond()
    {
        InputAction _seeBeyond = _playerInput.actions["SeeBeyond"];
        if (!GameManager.Instance.EerieObtained)
        {
            return;
        }
        else
        {
            if (_seeBeyond.WasPerformedThisFrame())
            {
                
                _anim.SetBool(_animSeeBeyondBool, !_anim.GetBool(_animSeeBeyondBool));
            }
        }      
    }

    public void FollowPlayer()
    {
        if (!_player.Grounded)
        {
            return;
        }

        if (Vector3.Distance(_player.transform.position, transform.position) < 1.3f)
        {
            _anim.SetBool(_animIdleBool, true);

        }
        else
        {
            _anim.SetBool(_animIdleBool, false);
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
            transform.LookAt(_player.transform.position);

            _speed = (Vector3.Distance(_player.transform.position, transform.position) > 5f ? 8f : Vector3.Distance(_player.transform.position, transform.position) > 3f ? 5f :2f );
        }
    }

    public void SeeBeyondActivation()
    {
        _seeBeyondLight.enabled = (_seeBeyondLight.enabled? false : true);        
    }

    public void StopTerrified()
    {
        if (GameManager.Instance.EerieObtained)
            _anim.SetBool(_animStopTerrifyBool, true);
    }

  
}
