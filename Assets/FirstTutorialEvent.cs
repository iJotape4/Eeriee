using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using StarterAssets;
using UnityEngine.UI;

public class FirstTutorialEvent : MonoBehaviour
{

    public InteractableObject _dialogues;
    public PlayerInput _playerInput;
    public InputActionMap _actionMap;
    public TextMeshProUGUI _textInScreen;
    public FirstPersonController _player;
    public DialogueController _dc;
    public Image _nextButton;
    // Start is called before the first frame update
    void Start()
    {
        _dialogues = GetComponent<InteractableObject>();     
        _textInScreen = UIManager.Instance._textinScreen;
        _dc = FindObjectOfType<DialogueController>();
        _nextButton = UIManager.Instance._nextButton;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIManager.Instance.NextButtonActivation(false);
             _playerInput.SwitchCurrentActionMap("Tutorial");
            _actionMap = _playerInput.currentActionMap;           
            StartCoroutine(TutorialManager());
        }
    }


    public IEnumerator TutorialManager()
    {
        InputAction TutorialAction = _playerInput.actions["TutorialAction"];
        while (_dialogues._texts.arrayTextos[0] != _textInScreen.text)
        {
            yield return new WaitForEndOfFrame();
        }
        UIManager.Instance._nextButton.enabled = true;
        TutorialAction.ApplyBindingOverride("<Keyboard>/space");

        while (!TutorialAction.WasPressedThisFrame())
        {
            yield return new WaitForEndOfFrame();
        }
        UIManager.Instance.NextButtonActivation(false);
        _dc.Nextphrase();
        while (_dialogues._texts.arrayTextos[1] != _textInScreen.text)
        {
            yield return new WaitForEndOfFrame();
        }
        
        TutorialAction.ApplyBindingOverride("<Keyboard>/2");

        while (!TutorialAction.WasPressedThisFrame())
        {
            yield return new WaitForEndOfFrame();
        }
        UIManager.Instance._Uicons[1].transform.parent.gameObject.SetActive(true);
        UIManager.Instance._Uicons[1].enabled = true;
        UIManager.Instance.activateUiCon(1);
        _player._input.currentWeapon = 1;
        _dc.Nextphrase();
        while (_dialogues._texts.arrayTextos[2] != _textInScreen.text)
        {
            yield return new WaitForEndOfFrame();
        }
        
        TutorialAction.ApplyBindingOverride("<Mouse>/leftButton");

        while (!TutorialAction.WasPressedThisFrame())
        {
            yield return new WaitForEndOfFrame();
        }
        _player.doAttack(1);
        _dc.Nextphrase();
        while (_dialogues._texts.arrayTextos[3] != _textInScreen.text)
        {
            yield return new WaitForEndOfFrame();
        }

        TutorialAction.ApplyBindingOverride("<Mouse>/rightButton");

        while (!TutorialAction.WasPressedThisFrame())
        {
            yield return new WaitForEndOfFrame();
        }
        _player.doAttack(2);
        _dc.Nextphrase();

        while (_dialogues._texts.arrayTextos[4] != _textInScreen.text)
        {
            yield return new WaitForEndOfFrame();
        }

        TutorialAction.ApplyBindingOverride("<Keyboard>/3");

        while (!TutorialAction.WasPressedThisFrame())
        {
            yield return new WaitForEndOfFrame();
        }
        UIManager.Instance._Uicons[2].enabled = true;
        UIManager.Instance.activateUiCon(2);
        _player._input.currentWeapon = 2;
        _dc.Nextphrase();

        while (_dialogues._texts.arrayTextos[5] != _textInScreen.text)
        {
            yield return new WaitForEndOfFrame();
        }

        TutorialAction.ApplyBindingOverride("<Mouse>/rightButton");
        TutorialAction.AddBinding("<Mouse>/leftButton");

        while (!TutorialAction.WasPressedThisFrame())
        {
            yield return new WaitForEndOfFrame();
        }
        _player.doAttack(1);
        _dc.Nextphrase();


        while (_dialogues._texts.arrayTextos[6] != _textInScreen.text)
        {
            yield return new WaitForEndOfFrame();
        }
        TutorialAction.ApplyBindingOverride("<Keyboard>/1");
        while (!TutorialAction.WasPressedThisFrame())
        {
            yield return new WaitForEndOfFrame();
        }
        UIManager.Instance._Uicons[0].enabled = true;
        UIManager.Instance.activateUiCon(0);
        _player._input.currentWeapon = 0;
        _dc.Nextphrase();

        _playerInput.SwitchCurrentActionMap("Dialogues");
        UIManager.Instance.NextButtonActivation(true);

        yield return null;
    }
}
