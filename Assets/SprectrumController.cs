using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprectrumController : MonoBehaviour
{
    public Transform _playerTarget;
    public Transform _spawnPoint;
    public GameObject _blueFire;

    [SerializeField] private float shotForce = 10f;
    // Start is called before the first frame update
    void Start()
    {
        _playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _spawnPoint = transform.GetChild(0);
        _blueFire = Resources.Load<GameObject>("Prefabs/BlueFire");
        LaunchFireBall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LaunchFireBall()
    {
        transform.LookAt(_playerTarget);
        GameObject FireBall;
        FireBall = Instantiate(_blueFire, _spawnPoint.transform);
        FireBall.transform.parent = null;
        FireBall.GetComponent<Rigidbody>().AddForce(_spawnPoint.forward * shotForce);
        FireBall.transform.LookAt(_playerTarget);      
    }
}
