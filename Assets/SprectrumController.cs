using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprectrumController : MonoBehaviour
{
    public Transform _playerTarget;
    public Transform _spawnPoint;
    public GameObject _blueFire;
    public CapsuleCollider capsuleCollider;

    public float shotCadency = 3f;
    private float counter;


    [SerializeField] private float shotForce = 15f;
    // Start is called before the first frame update
    void Start()
    {
        _playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _spawnPoint = transform.GetChild(0);
        _blueFire = Resources.Load<GameObject>("Prefabs/BlueFire");
        capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.EerieObtained)
        {
            capsuleCollider.enabled = true;

            counter += Time.deltaTime;
            if (counter > shotCadency)
            {
                LaunchFireBall();                
                counter = 0;          
            }
            if(counter > shotCadency / 2f)
            {
               // Move();
            }

        }

       
    }

    public void LaunchFireBall()
    {

        transform.LookAt(_playerTarget);
        GameObject FireBall;
        FireBall = Instantiate(_blueFire, _spawnPoint.transform);
        FireBall.transform.parent = null;
        FireBall.GetComponent<Rigidbody>().AddForce(_spawnPoint.forward * shotForce);
        FireBall.transform.LookAt(_playerTarget);

    }

    public void Move()
    {
        var position = new Vector3(Random.Range(-3.0f, 3.0f), 0, Random.Range(-3.0f, 3.0f));
        transform.position = (Vector3.Distance(_playerTarget.position, transform.position) > 5f?
            Vector3.MoveTowards(transform.position, _playerTarget.position, Time.deltaTime):
            Vector3.MoveTowards(transform.position, transform.position += position, Time.deltaTime));
        transform.LookAt(_playerTarget);
    }
}
