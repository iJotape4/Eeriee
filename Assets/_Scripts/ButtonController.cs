using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject elevator = null;
    [SerializeField] private Animator elevator2 = null;
    [SerializeField] private Animator externaldoors1 = null;
    [SerializeField] private Animator externaldoors2 = null;
    [SerializeField] private Animator externaldoors3 = null;
    [SerializeField] private Animator internaldoors1 = null;
    [SerializeField] private Animator internaldoors2 = null;
    public IEnumerator OnPress(GameObject button)
    {
        Animator buttonAnim = button.GetComponent<Animator>();
        //Animación del Botón.
        buttonAnim.SetBool("pressed", true);
        yield return new WaitForSeconds(1); //Retraso de 1 seg para volver a presionar.
        buttonAnim.SetBool("pressed", false);
        switch (button.tag)
        {
            case "upButton":
                //Cerrar Puertas.
                externaldoors3.SetBool("AbrirPuertas", false);
                if (elevator.transform.position.y == 3.5)
                {
                    internaldoors1.SetBool("AbrirPuertas", false);
                    internaldoors2.SetBool("AbrirPuertas", false);
                }
                //Retraso de 3 seg para subir.
                yield return new WaitForSeconds(3);
                elevator2.SetBool("subir", true);
                break;
            case "downButton":
                //Cerrar Puertas.
                externaldoors1.SetBool("AbrirPuertas", false);
                externaldoors2.SetBool("AbrirPuertas", false);
                if (elevator.transform.position.y == 32)
                {
                    internaldoors1.SetBool("AbrirPuertas", false);
                    internaldoors2.SetBool("AbrirPuertas", false);
                }
                //Retraso de 3 seg para bajar.
                yield return new WaitForSeconds(3);
                elevator2.SetBool("subir", false);
                break;
        }
    }
}
