using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeBeyond : MonoBehaviour
{
    public Camera _mainCamera;
    public Camera _seeBeyondCamera;
    public Light _seeBeyondLight;
    public int _cullingMask;
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _seeBeyondCamera = GetComponentInChildren<Camera>();
        _mainCamera.cullingMask -= (1 << LayerMask.NameToLayer("TransparentFX"));

        _seeBeyondLight = GetComponent<Light>();
        _seeBeyondLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        _seeBeyondCamera.enabled = (_seeBeyondLight.enabled ?  true : false);
        _mainCamera.enabled = (_seeBeyondCamera.enabled ? false : true);
    }
}
