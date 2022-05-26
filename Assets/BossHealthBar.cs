using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossHealthBar : Healthbar
{
    public  SubBossController _subBoss;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
       actualHealth = _subBoss._health;
        MaximHealth = _subBoss._health;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.InSubBossFight)
        {     
        actualHealth = _subBoss._health;
        healthbar.fillAmount = actualHealth / MaximHealth;
        }
    }


}
