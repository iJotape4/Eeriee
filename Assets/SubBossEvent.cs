using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubBossEvent : MonoBehaviour
{
    public SubBossController subBoss;
    public bool _battleTriggered =false;


    // Start is called before the first frame update
    void Awake()
    {
        subBoss = FindObjectOfType<SubBossController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!_battleTriggered)
        {
            GetComponent<BoxCollider>().enabled = false;
            _battleTriggered = true;
            GameManager.Instance.SubBossFight(true);
            UIManager.Instance.BossHealthBarActivation();
            subBoss.gameObject.SetActive(true);
        }
    }
}
