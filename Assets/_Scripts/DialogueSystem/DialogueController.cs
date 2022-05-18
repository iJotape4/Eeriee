using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    private Animator _anim;
    private Queue<string> _dialoguesQueue;
    TextsDictionary _text;
    private string _animEnableBool = "Enable";
    [SerializeField] TextMeshProUGUI _textInScreen;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _dialoguesQueue = new Queue<string>();
    }

    public void ActivateDialogue( TextsDictionary objectText)
  {
        _anim.SetBool(_animEnableBool, true);
        _text = objectText;
    }

    public void ActivateText()
    {
        _dialoguesQueue.Clear();
        foreach (string savedText in _text.arrayTextos)
        {
            _dialoguesQueue.Enqueue(savedText);
        }

        Nextphrase();
    }

    public void Nextphrase()
    {
        if(_dialoguesQueue.Count == 0)
        {
            CloseDialogue();
            return;
        }

        string currentPhrase = _dialoguesQueue.Dequeue();
        _textInScreen.text = currentPhrase;
        StartCoroutine(ShowCharacters(currentPhrase));
    }

    public void CloseDialogue()
    {
        _anim.SetBool(_animEnableBool, false);
    }


    IEnumerator ShowCharacters (string textToShow)
    {
        _textInScreen.text = "";
        foreach (char character in textToShow.ToCharArray())
        {
            _textInScreen.text += character;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
