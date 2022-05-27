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
    public Transform _playerDirection;
  

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIManager.Instance.UpdateHealth(additionalDmg);
            Ray directionRay = new Ray(transform.position, (_playerDirection.position - transform.position).normalized);
            
            Debug.DrawRay(directionRay.origin, directionRay.direction, Color.red, 2f);
           
           // Physics.ray
            StartCoroutine(PlayerPush(directionRay.direction));
        }

    }

    private IEnumerator PlayerPush(Vector3 direction)
    {
        _playerRB.GetComponent<CharacterController>().enabled = false;
        _playerRB.GetComponent<Rigidbody>().AddForce(direction *pushforce * Time.deltaTime, ForceMode.Impulse);
        yield return new WaitForSeconds(1f);
        _playerRB.GetComponent<CharacterController>().enabled = true;
    }
}
