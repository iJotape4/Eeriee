using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubBossController : ZombieController
{
    private string _animAttackInt = "Attack";
    // Start is called before the first frame update
     void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetBool(_animStunnedBool, _stunned);

        if (_health <= 0)
        {
            base.callDeath();
        }
        Pursuit();
    }

    protected void Pursuit()
    {
        if (!_player.Grounded || !_canPursuit)
            return;

        base.PursuitPlayer();

        foreach (string animation in animZombieAttacks)
        {
            if (_anim.GetCurrentAnimatorStateInfo(0).IsName(animation))
            {
                //This stops the zombie to move while is attacking
                return;
            }
        }

        _anim.SetInteger(_animAttackInt, Random.Range(2, 5)) ;
    }
}
