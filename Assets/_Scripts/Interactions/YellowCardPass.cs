using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowCardPass : Pickable
{
    public Light[] _classroomLights;

    public override void Interact()
    {
        base.Interact();
        GameManager.Instance.YellowCardObtention(true);
        GetComponent<AudioSource>().Play();

        foreach(Light light in _classroomLights)
        {
            light.enabled = false;
        }

        GameManager.Instance.NextEvent();
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, 2f);
    }


}
