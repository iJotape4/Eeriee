using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    Transform cam;
    ButtonController buttonObj;
    private float range = 3f;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    private void OnInteract(InputValue value)
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, range))
        {
            switch (hit.transform.gameObject.tag)
            {
                case "downButton":
                case "upButton":
                    Debug.Log("Botón del Ascensor");
                    buttonObj = hit.transform.gameObject.GetComponent<ButtonController>();
                    StartCoroutine(buttonObj.OnPress(hit.transform.gameObject));
                    break;
            }
        }
    }
}
