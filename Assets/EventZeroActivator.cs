using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class EventZeroActivator : MonoBehaviour
{
    public FirstPersonController _player;
    void Start()
    {
        _player.GetComponent<AudioSource>().enabled = true;
    }
}
