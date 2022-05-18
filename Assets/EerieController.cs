using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EerieController : MonoBehaviour
{

    [Header("Animation Parameters")]
    public Animator _anim;
    private string _animSeeBeyondBool = "SeeBeyond";
    private Light _seeBeyondLight;

    public PlayerInput _playerInput;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _seeBeyondLight = GameObject.Find("SeeBeyondLigth").GetComponent<Light>();
        _playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        SeeBeyond();
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

    public void SeeBeyondActivation()
    {
        _seeBeyondLight.enabled = (_seeBeyondLight.enabled? false : true);        
    }

  
}
