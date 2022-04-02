using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    [SerializeField] private Animator elevator = null;
    [SerializeField] private Animator externaldoors1 = null;
    [SerializeField] private Animator externaldoors2 = null;
    [SerializeField] private Animator externaldoors3 = null;
    [SerializeField] private Animator internaldoors1 = null;
    [SerializeField] private Animator internaldoors2 = null;
    [SerializeField] private GameObject[] emissions; 
    void Awake()
    {
        emissions = GameObject.FindGameObjectsWithTag("emission");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ascensor")
        {
            if (this.gameObject.name == "UpperTrigger")
            {
                //Reset Emissions para cada botón
                foreach (GameObject mat in emissions)
                {
                    mat.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
                }
                //Abrir todas las puertas exteriorres e interiores
                externaldoors1.SetBool("AbrirPuertas", true);
                externaldoors2.SetBool("AbrirPuertas", true);
                internaldoors1.SetBool("AbrirPuertas", true);
                internaldoors2.SetBool("AbrirPuertas", true);
                elevator.SetBool("subir", true);
            }
            else if (this.gameObject.name == "BottomTrigger")
            {
                //Reset Emissions para cada botón
                foreach (GameObject mat in emissions)
                {
                    mat.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
                }
                //Abrir las puertas exteriorres e interiores que dan a camino
                externaldoors3.SetBool("AbrirPuertas", true);
                internaldoors1.SetBool("AbrirPuertas", true);
                elevator.SetBool("subir", false);
            }
        }
    }
}
