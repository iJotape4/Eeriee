using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DesktopEvent : Interactable
{

    private float _hackingVelocity =10f;
    private PlayerInput _playerInput;
    private Image _hackingBar;
    private BoxCollider _bc;
    public AudioSource _keyboardSFX;
    public BoxCollider _spectrumEvent;

    public void Start()
    {
        _playerInput = FindObjectOfType<PlayerInput>();
        _bc = GetComponent<BoxCollider>();
        _keyboardSFX = GetComponent<AudioSource>();
        _keyboardSFX.enabled = false;
    }

    public override void Interact()
    {
        base.Interact();
        _hackingBar = UIManager.Instance.Hacking();
        StartCoroutine(Hacking());
       
    }

    public IEnumerator Hacking()
    {
        _keyboardSFX.enabled = true;
        _playerInput.SwitchCurrentActionMap("Tutorial");
        _hackingBar.color = Color.gray;

        _hackingBar.transform.parent.gameObject.SetActive(true);
        _hackingBar.fillAmount = 0;
        while (_hackingBar.fillAmount != 1)
        {
            _hackingBar.fillAmount += 0.1f / _hackingVelocity;
            yield return new WaitForEndOfFrame();          
        }
        _keyboardSFX.enabled = false;
        _hackingBar.transform.parent.gameObject.SetActive(false);
        _playerInput.SwitchCurrentActionMap("Player");
        _bc.enabled = false;
        GameManager.Instance.NextEvent();
        GameManager.Instance.TheCellDoorsAreOpened();
        _spectrumEvent.enabled = true;
    }
}
