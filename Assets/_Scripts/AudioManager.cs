using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource _backgroundMainAudio;
    public AudioSource _eerieAudio;
    public AudioSource _thanks4PlayingAudio;
    private void Awake()
    {
        if (AudioManager.Instance == null)
        {
            AudioManager.Instance = this.GetComponent<AudioManager>();
        }
        else if (AudioManager.Instance != null && AudioManager.Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        // DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        _backgroundMainAudio = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        _eerieAudio = GameObject.FindGameObjectWithTag("Eerie").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void muteSounds()
    {
        _eerieAudio.enabled = false ;
        _backgroundMainAudio.enabled = false;
    }
}
