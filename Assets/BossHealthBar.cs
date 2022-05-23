using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossHealthBar : Healthbar
{
    private SubBossController _subBoss;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        _subBoss = GameObject.Find("SubBoss").GetComponent<SubBossController>();
        actualHealth = _subBoss._health;
        MaximHealth = _subBoss._health;
    }

    // Update is called once per frame
    void Update()
    {
        actualHealth = _subBoss._health;
        healthbar.fillAmount = actualHealth / MaximHealth;
    }


}