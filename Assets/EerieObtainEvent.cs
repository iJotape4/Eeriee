using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class EerieObtainEvent : MonoBehaviour
{
    public BoxCollider _eventTrigger;
    public FirstPersonController _player;
    private GameObject _eerie;
    private bool _isLookingEerie = false;

    // Start is called before the first frame update
    void Start()
    {
        _eventTrigger = GetComponent<BoxCollider>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        _eerie = GameObject.FindGameObjectWithTag("Eerie");
    }

    // Update is called once per frame
    void Update()
    {
        if (!_eventTrigger.enabled)
        {
            if (!_isLookingEerie)
            {
                _player.transform.LookAt(_eerie.transform);
                _isLookingEerie = true;
            }
           
        }

        if (GetComponent<InteractableObject>()._finishedEvent)
        {
            GameManager.Instance.eerieObtention();
            _eerie.GetComponent<EerieController>().StopTerrified();
            _eerie.GetComponent<AudioSource>().Stop();
        }
    }
}
