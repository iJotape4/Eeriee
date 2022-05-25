using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalEvent : MonoBehaviour
{
    private InteractableObject _io;
    public AudioSource _finalBossAudio;
    // Start is called before the first frame update
    void Start()
    {
        _io = GetComponent<InteractableObject>();
        _finalBossAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_io._finishedEvent)
        {
            AudioManager.Instance.muteSounds();
            _finalBossAudio.enabled = false;
            UIManager.Instance._thanks4PlayingPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }   
    }
}
