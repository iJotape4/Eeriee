using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DialogueController : MonoBehaviour
{
    private Animator _anim;
    private Queue<string> _dialoguesQueue;
    private Queue<Sprite> _avatarsQueue;

    private PlayerInput _playerInput;
    private InputAction _next;
    private InputAction _skipAll;

    TextsDictionary _text;
    private string _animEnableBool = "Enable";
    [SerializeField] TextMeshProUGUI _textInScreen;
    [SerializeField] Image _avatarInScreen;

    private bool _finisedText =true;
    public GameObject currentEvent;


    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _dialoguesQueue = new Queue<string>();
        _avatarsQueue = new Queue<Sprite>();

        _playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        _next = _playerInput.actions["Next"];
        _skipAll = _playerInput.actions["SkipAll"];

        _avatarInScreen = GameObject.Find("CharacterImage").GetComponent<Image>();
    }

    private void Update()
    {
        if (_skipAll.WasPerformedThisFrame())
        {
            CloseDialogue();
        }
        else
        {
            if (_next.WasPressedThisFrame())
            {
                Nextphrase();
            }
        }     
    }

    public void ActivateDialogue( TextsDictionary objectText, GameObject _event)
  {
        _anim.SetBool(_animEnableBool, true);
        _text = objectText;
        currentEvent = _event;
    }

    public void ActivateText()
    {
        _dialoguesQueue.Clear();
        _avatarsQueue.Clear();
        foreach (string savedText in _text.arrayTextos)
        {
            _dialoguesQueue.Enqueue(savedText);
        }

        foreach (Sprite avatar in _text.arrayAvatars)
        {
            _avatarsQueue.Enqueue(avatar);
        }
        _playerInput.SwitchCurrentActionMap("Dialogues");
        Nextphrase();
    }

    public void Nextphrase()
    {
        if (_finisedText) 
        {
            _finisedText = false;
            if (_dialoguesQueue.Count == 0)
            {
                CloseDialogue();
                return;
            }

            string currentPhrase = _dialoguesQueue.Dequeue();
            if(_avatarsQueue.Count > 0)
            {
                _avatarInScreen.sprite = _avatarsQueue.Dequeue();
            }         
            _textInScreen.text = currentPhrase;
                StartCoroutine(ShowCharacters(currentPhrase));
        }
        
    }

    public void CloseDialogue()
    {
        _anim.SetBool(_animEnableBool, false);
        _playerInput.SwitchCurrentActionMap("Player");
        currentEvent.GetComponent<InteractableObject>()._finishedEvent = true;
    }


    IEnumerator ShowCharacters (string textToShow)
    {
        _textInScreen.text = "";
        foreach (char character in textToShow.ToCharArray())
        {
            _textInScreen.text += character;
            yield return new WaitForSeconds(0.02f);
        }

        _finisedText = true;
    }
}
