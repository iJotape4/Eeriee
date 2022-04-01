using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    [SerializeField] private Animator externaldoors1 = null;
    [SerializeField] private Animator externaldoors2 = null;
    [SerializeField] private Animator externaldoors3 = null;
    [SerializeField] private Animator internaldoors1 = null;
    [SerializeField] private Animator internaldoors2 = null;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ascensor")
        {
            if (this.gameObject.name == "UpperTrigger")
            {
                //Abrir todas las puertas exteriorres e interiores
                externaldoors1.SetBool("AbrirPuertas", true);
                externaldoors2.SetBool("AbrirPuertas", true);
                internaldoors1.SetBool("AbrirPuertas", true);
                internaldoors2.SetBool("AbrirPuertas", true);
            }
            else if (this.gameObject.name == "BottomTrigger")
            {
                //Abrir las puertas exteriorres e interiores que dan a camino
                externaldoors3.SetBool("AbrirPuertas", true);
                internaldoors1.SetBool("AbrirPuertas", true);
            }
        }
    }
}
