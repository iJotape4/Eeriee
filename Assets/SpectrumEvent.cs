using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class SpectrumEvent : MonoBehaviour
{

    public EerieController _eerie;
    public InteractableObject _dialogues;
    public GameObject _spectrum;
    public PlayerInput _playerInput;
    public InputActionMap _actionMap;
    public TextMeshProUGUI _textInScreen;
    public DialogueController _dc;

    // Start is called before the first frame update
    void Start()
    {
        _eerie = GameObject.FindGameObjectWithTag("Eerie").GetComponent<EerieController>();
        _dialogues = GetComponent<InteractableObject>();
        _playerInput = FindObjectOfType<PlayerInput>();
        _textInScreen = UIManager.Instance._textinScreen;
        _dc = FindObjectOfType<DialogueController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {           
            _spectrum = Instantiate(Resources.Load<GameObject>("Prefabs/SpectrumParent"), transform.position, transform.rotation) as GameObject;
            _spectrum.GetComponentInChildren<Animator>().enabled = false;
            _playerInput.SwitchCurrentActionMap("Tutorial");
            _eerie.CallSeeBeyond();
            _actionMap = _playerInput.currentActionMap;
            StartCoroutine(EerieTutorial());
        }
    }


    public IEnumerator EerieTutorial()
    {
       
        UIManager.Instance._holdNextButton.enabled = false;
        InputAction TutorialAction = _playerInput.actions["TutorialAction"];
        TutorialAction.ApplyBindingOverride("<Keyboard>/space");

        for (int i=0; i<=2;i++)
        {
            while (_dialogues._texts.arrayTextos[i] != _textInScreen.text)
            {
                yield return new WaitForEndOfFrame();
            }

            while (!TutorialAction.WasPressedThisFrame())
            {
                yield return new WaitForEndOfFrame();
            }
            _dc.Nextphrase();
        }
        UIManager.Instance.NextButtonActivation(false);
        TutorialAction.ApplyBindingOverride("<Keyboard>/q");
        UIManager.Instance.BlueEyeActivation();
        while (!TutorialAction.WasPressedThisFrame())
        {
            yield return new WaitForEndOfFrame();
        }
        _eerie.CallSeeBeyond();
        _dc.Nextphrase();
        UIManager.Instance.NextButtonActivation(true);
        _playerInput.SwitchCurrentActionMap("Dialogues");

        while (!_dialogues._finishedEvent)
        {
            yield return new WaitForEndOfFrame();
        }
        _spectrum.GetComponentInChildren<Animator>().enabled = true;
    }
}
