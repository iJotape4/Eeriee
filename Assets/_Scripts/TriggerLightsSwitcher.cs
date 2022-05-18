using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TriggerLightsSwitcher : MonoBehaviour
{
    [SerializeField] private BoxCollider _triggerBox;
    [SerializeField] private Light _light;

    void Start()
    {
        _triggerBox = GetComponent<BoxCollider>();
        _triggerBox.isTrigger = true;
        _light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        _triggerBox.enabled = (_light.enabled ? false : true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.Instance.UpdateHealth(-0.1f);
        }
     
    }
}
