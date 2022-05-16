using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{

    public static WaterController Instance;
    public Rigidbody _waterRB;
    public Transform _bottleCap;

    [SerializeField] private float shotForce =2000f;

    private void Awake()
    {
        WaterController.Instance = this.GetComponent<WaterController>();
    }

    private void Start()
    {
       
    }


    public void LaunchWater()
    {
        _bottleCap = GameObject.Find("BottleCap").GetComponent<Transform>();
        _waterRB = Resources.Load<GameObject>("Prefabs/WaterJet").GetComponent<Rigidbody>();

        Rigidbody _waterInstance;
        _waterInstance = Instantiate(_waterRB, _bottleCap.position, _bottleCap.rotation) as Rigidbody;
        _waterInstance.AddForce(_bottleCap.forward* shotForce);
    }
}
