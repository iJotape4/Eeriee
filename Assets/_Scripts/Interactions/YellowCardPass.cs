using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowCardPass : Pickable
{
    public override void Interact()
    {
        base.Interact();
        GameManager.Instance.YellowCardObtention(true);
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, 2f);
    }


}
