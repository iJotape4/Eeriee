using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class ZombieController : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody _rb;

    private NavMeshAgent _zombie;
    public Transform _player;
    public float _range = 20f;
    public float _speed = 1f;
    public bool _zombieRun = false;

    public Transform[] points;
    public int actualPatrolPoint = 0;

    #region  Animations Dictionary
    [Header("Animations Dictionary")]
    private string animZombieWalk = "Anim_ZombieWalk";
    private string animZombieAttack = "Anim_ZombieAttack";
    private string animZombieCrawl =  "Anim_ZombieCrawl";
    private string animZombieCrawlRunning = "Anim_ZombieCrawlRunning";
    private string animZombieDeath =  "Anim_ZombieDeath";
    private string animZombieBitting = "Anim_ZombieBiitting";
    private string animZombieBitting2  = "Anim_ZombieBitting2";
    private string animZombieIdle = "Anim_ZombieIdle";
    private string animZombieDying = "Anim_ZombieDiying";
    private string animZombieNeckBite = "Anim_ZombieNeckBite";
    private string animZombieScream = "Anim_ZombieScream";
    private string animZombieRun = "Anim_ZombieRun";


    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _zombie = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


        foreach (Transform GO  in GetComponentsInChildren<Transform>())
        { 
            if(GO.gameObject.name == "PatrolPoints")
            {
                points = GO.GetComponentsInChildren<Transform>();

                for ( int i = 0; i < points.Length-1; i++)
                {
                    points[i] = points[i + 1];
                }
                GO.SetParent(null);
            }
        }
        
        _zombie.autoBraking = false;
       
    }

    // Update is called once per frame
    void Update()
    {
     
        PlayerDetection();
    }
    public IEnumerator GotoNextPoint(int patrolPoint)
    {
        Vector3 target = points[patrolPoint].transform.position;
       _anim.Play(animZombieWalk);
        transform.LookAt(points[patrolPoint]);
        transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.z == target.z)
        {
            actualPatrolPoint++;
        }

        if (actualPatrolPoint == points.Length )
            actualPatrolPoint = 0;

        yield return null;

        /* If NavMesh is implemented
        _zombie.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
        */
    }


    void Patrol()
    {

        if (points.Length == 0)
        {
            _anim.Play(animZombieIdle);
            return;
        }

           StartCoroutine(GotoNextPoint(actualPatrolPoint));
       
    }

    void PlayerDetection()
    {
        if (Vector3.Distance(_player.position, transform.position) <= _range)

        {
            PursuitPlayer();
        }
        else
        {
            Patrol();
            
        }
    }
    void PursuitPlayer()
    {
        if (_zombieRun)
        {
            _speed = 5f;
            _anim.Play(animZombieRun);
        }
        else
        {
            _speed = 0.2f;
            _anim.Play(animZombieWalk);
        }
        transform.position = Vector3.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
    }



}
