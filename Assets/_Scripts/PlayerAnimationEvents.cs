using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public Rigidbody _waterRB;
    public Transform _bottleCap;

    public GameObject _smartWatch;

    [SerializeField] private float shotForce = 2000f;

    public void LaunchWater()
    {
        _bottleCap = GameObject.Find("BottleCap").GetComponent<Transform>();
        _waterRB = Resources.Load<GameObject>("Prefabs/WaterJet").GetComponent<Rigidbody>();

        Rigidbody _waterInstance;
        _waterInstance = Instantiate(_waterRB, _bottleCap.position, _bottleCap.rotation) as Rigidbody;
        _waterInstance.AddForce(_bottleCap.forward * shotForce);
    }

    public void SmartWatch()
    {
        UIManager.Instance.SmartWatch();
    }

    public void DisableSmartWatchCamera()
    {

    }




    }
