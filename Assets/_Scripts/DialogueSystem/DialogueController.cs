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
    private Queue<Sprite> _dBoxQueue;

    private PlayerInput _playerInput;
    private InputAction _next;
    private InputAction _skipAll;

    TextsDictionary _text;
    private string _animEnableBool = "Enable";
    [SerializeField] TextMeshProUGUI _textInScreen;
    [SerializeField] Image _avatarInScreen;
    [SerializeField] Image _dBoxInScreen;
    [SerializeField] Image _nextButton;
    [SerializeField] Image _holdNextButton;

    private Sprite _transparentSprite;
    private Sprite _sprNextButton;
    private Sprite _sprHoldNextButton;

    private bool _finisedText =true;
    public GameObject currentEvent;


    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _dialoguesQueue = new Queue<string>();
        _avatarsQueue = new Queue<Sprite>();
        _dBoxQueue = new Queue<Sprite>();

        _playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        _next = _playerInput.actions["Next"];
        _skipAll = _playerInput.actions["SkipAll"];
        _transparentSprite = Resources.Load<Sprite>("Sprites/TransparentSprite");

        _avatarInScreen = GameObject.Find("CharacterImage").GetComponent<Image>();
  
        _dBoxInScreen = this.GetComponent<Image>();
        _nextButton = transform.GetChild(2).GetComponent<Image>();
        _holdNextButton = transform.GetChild(3).GetComponent<Image>();

        _sprNextButton = _nextButton.sprite;
        _sprHoldNextButton = _holdNextButton.sprite;

        CleanDialoguePanel();
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

        foreach (Sprite dBox in _text.arrayDBoxes)
        {
            _dBoxQueue.Enqueue(dBox);
        }

        _playerInput.SwitchCurrentActionMap("Dialogues");
        
        Nextphrase();
    }

    public void CleanDialoguePanel()
    {
        _avatarInScreen.sprite = _transparentSprite;
        _dBoxInScreen.sprite = _transparentSprite;
        _nextButton.sprite = _transparentSprite;
        _holdNextButton.sprite = _transparentSprite;
       
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

            if (_dBoxQueue.Count > 0)
            {
               _dBoxInScreen.sprite = _dBoxQueue.Dequeue();
            }
            
            _textInScreen.text = currentPhrase;
            _nextButton.sprite = _sprNextButton;
            _holdNextButton.sprite = _sprHoldNextButton;
             StartCoroutine(ShowCharacters(currentPhrase));
        }
        
    }

    public void CloseDialogue()
    {
        _anim.SetBool(_animEnableBool, false);
        _playerInput.SwitchCurrentActionMap("Player");
        currentEvent.GetComponent<InteractableObject>()._finishedEvent = true;
        CleanDialoguePanel();
        
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
