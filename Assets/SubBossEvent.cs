using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubBossEvent : MonoBehaviour
{
    public SubBossController subBoss;
    public bool _battleTriggered =false;

    // Start is called before the first frame update
    void Start()
    {
        subBoss = FindObjectOfType<SubBossController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<InteractableObject>()._finishedEvent && !_battleTriggered)
        {
            _battleTriggered = true;
            GameManager.Instance.SubBossFight(true);
            UIManager.Instance.BossHealthBarActivation();
            subBoss.gameObject.SetActive(true);
                    
        }
    }
}
