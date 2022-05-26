using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

//[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]

public class ZombieController : MonoBehaviour
{

    public Rigidbody _rb;

    public float _health = 100f;
    public bool _stunned;
    public bool _hitted;
    public bool _canPursuit = true;

    public float _attackzone =1.2f;

    public float _range = 20f;
    public float _speed = 1f;
    public float _walkSpeed = 2f;
    public float _runSpeed = 5f;

    public bool _zombieRunner = false;

    public float pushForce;

    private Transform[] points;
    private int actualPatrolPoint = 0;
    public SkinnedMeshRenderer[] meshRenderers;

   // public NavMeshAgent _zombie;

    public FirstPersonController _player;
    #region  Animations Dictionary
    [Header("Animation Parameters")]
    public Animator _anim;
    protected string _animPatrollingBool = "Patrolling";
    protected string _animPlayerDetectedBool = "PlayerDetected";
    protected string _animIdleBool = "Idle";
    protected string _animAttackZoneBool = "AttackZone";
    protected string _animRunnerZombieBool = "RunnerZombie";
    protected string _animStunnedBool = "Stunned";
    protected string _animHittedTrigger = "Hitted";
    protected string _animDeathTrigger = "Death";

    [Header("Animations Dictionary")]
    protected string animZombieWalk = "Anim_ZombieWalk";
    protected string [] animZombieAttacks = { "Anim_ZombieAttack", "Anim_Zombie2Attack", "Anim_SubBossAttack1", "Anim_SubBossAttack2",
                                            "Anim_SubBossAttack3", "Anim_SubBossAttack4"};
 
    protected string animZombieCrawl =  "Anim_ZombieCrawl";
    protected string animZombieCrawlRunning = "Anim_ZombieCrawlRunning";
    protected string animZombieDeath =  "Anim_ZombieDeath";
    protected string animZombieBitting = "Anim_ZombieBiitting";
    protected string animZombieBitting2  = "Anim_ZombieBitting2";
    protected string animZombieIdle = "Anim_ZombieIdle";
    protected string animZombieDying = "Anim_ZombieDiying";
    protected string animZombieNeckBite = "Anim_ZombieNeckBite";
    protected string animZombieScream = "Anim_ZombieScream";
    protected string animZombieRun = "Anim_ZombieRun";

  

    #endregion


    // Start is called before the first frame update
    protected void Start()
    {
       // _zombie = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>() ;

        meshRenderers= GetComponentsInChildren<SkinnedMeshRenderer>();

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
        
        //_zombie.autoBraking = false;
       
    }

    // Update is called once per frame
    void Update()
    {   
        PlayerDetection();
        _anim.SetBool(_animStunnedBool, _stunned);
        _anim.SetBool(_animRunnerZombieBool, _zombieRunner);

        if (_health<=0)
        {
            callDeath(); 
        }
    }


    protected void callDeath()
    {
        _stunned = false;
        _canPursuit = false;
        foreach (CapsuleCollider col in GetComponentsInChildren<CapsuleCollider>())
        {
            col.enabled = false;
        }
        _anim.SetTrigger(_animDeathTrigger);
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
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2.5f, LayerMask.GetMask("Default")))
        {
            return;
        }

        if (Vector3.Distance(_player.transform.position, transform.position) <= _range)

        {
            _anim.SetBool(_animPlayerDetectedBool, true);

            if (!_player.Grounded || !_canPursuit)
                return;
            PursuitPlayer();
        }
        else
        {
            _anim.SetBool(_animPlayerDetectedBool, false);
            Patrol();         
        }
    }
    public void PursuitPlayer()
    {
        if (Vector3.Distance(_player.transform.position, transform.position) < _attackzone)
        {
            _anim.SetBool(_animAttackZoneBool, true);
        }
        else
        {
            _anim.SetBool(_animAttackZoneBool, false);

            foreach (string animation in animZombieAttacks)
            {
                if (_anim.GetCurrentAnimatorStateInfo(0).IsName(animation))
                {
                    //This stops the zombie to move while is attacking
                    return;
                }
            }
         
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
            transform.LookAt(_player.transform.position);
            
        }
        if (_zombieRunner)
        {
            _speed = _runSpeed;
        }
        else
        {
            _speed = _walkSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "HolyWater")
        {
            HolyWaterHit();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Weapon" ||  collision.gameObject.tag == "Projectile")
        {
        _hitted = true;
        _canPursuit = false;
        _health -= 10;     
            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Weapon" || collision.gameObject.tag == "Projectile")  
        {
            _hitted = false;
            _anim.SetTrigger(_animHittedTrigger);          
        }
    }

    public void HolyWaterHit()
    {
        _health -= 10;
        _stunned = true;
        _canPursuit = false;
    }

    public void StunEnds()
    {
        _stunned = false;
       _canPursuit = true;
    }

    protected void OnDeath()
    {
        Dispel();

    }

    public void Dispel()
    {
        foreach ( EnemyDispeler ED in GetComponentsInChildren<EnemyDispeler>())
        {
            ED.UpdateMaterialsArray();
        }
    }
}



