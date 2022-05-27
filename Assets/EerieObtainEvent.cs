using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class EerieObtainEvent : MonoBehaviour
{
    public BoxCollider _eventTrigger;
    public GameObject _eerie;

    // Start is called before the first frame update
    void Start()
    {
        _eventTrigger = GetComponent<BoxCollider>();

        if (GameManager.Instance.EerieObtained)
        {
            _eventTrigger.enabled = false;
        }
    }

    // Update is called once per frame


    private void OnTriggerEnter(Collider other)
    {
        
    }

   public IEnumerator EerieEvent()
    {

        yield return new WaitForSeconds(4f);
        GameManager.Instance.eerieObtention();
        _eerie.GetComponent<EerieController>().StopTerrified();
        _eerie.GetComponent<AudioSource>().Stop();
    }
}
