using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//[RequireComponent(typeof(Image))]

public class Healthbar : MonoBehaviour
{

    public Image healthbar;
    [SerializeField] protected float actualHealth =100f;
    [SerializeField] protected float MaximHealth = 100f;

    // Start is called before the first frame update
    protected void Start()
    {
        healthbar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.fillAmount = actualHealth / MaximHealth;   
    }

    public void updateHealthBar(float damage)
    {
        actualHealth += damage;
    }
}
