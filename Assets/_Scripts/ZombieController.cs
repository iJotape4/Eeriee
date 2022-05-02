using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ZombieController : MonoBehaviour
{
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Patrol()
    {

    }

    void PlayerDetection()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            UIManager.Instance.UpdateHealth(-10);
            
        }
    }
}
