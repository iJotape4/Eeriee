using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

[RequireComponent(typeof(CapsuleCollider))]
public class PlayerPusher : MonoBehaviour
{

    public float pushforce;
    public float additionalDmg;
    public FirstPersonController _playerRB;

    private void Start()
    {
        _playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIManager.Instance.UpdateHealth(additionalDmg);
            StartCoroutine(PlayerPush());
        }

    }

    private IEnumerator PlayerPush()
    {
        _playerRB.GetComponent<CharacterController>().enabled = false;
        _playerRB.GetComponent<Rigidbody>().AddForce(Vector3.right * pushforce * Time.deltaTime, ForceMode.Impulse);
        yield return new WaitForSeconds(1f);
        _playerRB.GetComponent<CharacterController>().enabled = true;
    }
}
