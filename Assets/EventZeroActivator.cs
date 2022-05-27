using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class EventZeroActivator : MonoBehaviour
{
    public FirstPersonController _player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _player.GetComponent<AudioSource>().enabled = true;
            UIManager.Instance.AwakePlayer();
        }
    }
}
