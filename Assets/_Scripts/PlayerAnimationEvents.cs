using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerAnimationEvents : MonoBehaviour
{
    public Rigidbody _waterRB;
    public Transform _bottleCap;

    public GameObject _smartWatch;
    public FirstPersonController _player;

    [SerializeField] private float shotForce = 2000f;


    public void LaunchWater()
    {
        _bottleCap = GameObject.Find("BottleCap").GetComponent<Transform>();
        _waterRB = Resources.Load<GameObject>("Prefabs/WaterJet").GetComponent<Rigidbody>();

        Rigidbody _waterInstance;
        _waterInstance = Instantiate(_waterRB, _bottleCap.position, _bottleCap.rotation) as Rigidbody;
        _waterInstance.AddForce(_bottleCap.forward * shotForce);
    }

    public void SmartWatch()
    {
        UIManager.Instance.SmartWatch();
    }

    public void DisableSmartWatch()
    {
        GetComponent<Animator>().SetBool("SmartWatch", false);
    }

    public void EnableBibleCollider()
    {
        GameObject _playerCurrentWeapon = _player.currentWeapon;

        if (_playerCurrentWeapon.name == ("Bible"))
            _playerCurrentWeapon.GetComponent<BoxCollider>().enabled = true;
    }

    public void DisableBibleCollider()
    {
        GameObject _playerCurrentWeapon = _player.currentWeapon;

        if (_playerCurrentWeapon.name == ("Bible"))
            _playerCurrentWeapon.GetComponent<BoxCollider>().enabled = false;
    }


}
