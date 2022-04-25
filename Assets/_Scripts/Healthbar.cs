using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//[RequireComponent(typeof(Image))]

public class Healthbar : MonoBehaviour
{

    public Image healthbar;
    public float actualHealth;
    public float MaximHealth;


    // Start is called before the first frame update
    void Start()
    {
        healthbar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.fillAmount = actualHealth / MaximHealth;   
    }
}
