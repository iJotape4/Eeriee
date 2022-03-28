using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Camera : MonoBehaviour
{
    public Camera _cam;
    [Range(0.1f, 2.0f)]
    public float sensivity;
    public bool invertXaxis;
    public bool invertYaxis;


    private void FixedUpdate()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        h = (invertXaxis) ? (-h) : h;
        v = (invertYaxis) ? (-v) : v;

        if (h != 0)
            transform.Rotate(Vector3.up, h * 90 * sensivity * Time.deltaTime); 

        if (v != 0)
            _cam.transform.RotateAround(transform.position, transform.right, v * 90 * sensivity * Time.deltaTime);

        Vector3 ea = _cam.transform.rotation.eulerAngles;
        _cam.transform.rotation = Quaternion.Euler(new Vector3(ea.x, ea.y, 0));

    }

    public Vector3 GetForwardDirection()
    {
        return _cam.transform.forward;
    }
}
