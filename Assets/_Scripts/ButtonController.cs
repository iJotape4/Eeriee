using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject elevator = null;
    [SerializeField] private Animator elevator2 = null;
    [SerializeField] private Animator externaldoors1 = null;
    [SerializeField] private Animator externaldoors2 = null;
    [SerializeField] private Animator externaldoors3 = null;
    [SerializeField] private Animator internaldoors1 = null;
    [SerializeField] private Animator internaldoors2 = null;
    [SerializeField] private Object[] lightRings;
    public IEnumerator OnPress(GameObject button)
    {
        Animator buttonAnim = button.GetComponent<Animator>();
        GameObject label = button.transform.GetChild(0).gameObject;
        GameObject ring = button.transform.parent.gameObject.transform.GetChild(1).gameObject;
        label.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        ring.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        //Animación del Botón.
        buttonAnim.SetBool("pressed", true);
        yield return new WaitForSeconds(1); //Retraso de 1 seg para volver a presionar.
        buttonAnim.SetBool("pressed", false);
        switch (button.tag)
        {
            case "upButton":
                if (elevator.transform.position.y == 3.5)
                {
                    //Cerrar Puertas.
                    externaldoors3.SetBool("AbrirPuertas", false);
                    internaldoors1.SetBool("AbrirPuertas", false);
                    internaldoors2.SetBool("AbrirPuertas", false);
                    //Retraso de 3 seg para subir.
                    yield return new WaitForSeconds(3);
                    elevator2.SetBool("subir", true);
                }
                break;
            case "downButton":
                if (elevator.transform.position.y == 32)
                {
                    //Cerrar Puertas.
                    externaldoors1.SetBool("AbrirPuertas", false);
                    externaldoors2.SetBool("AbrirPuertas", false);
                    internaldoors1.SetBool("AbrirPuertas", false);
                    internaldoors2.SetBool("AbrirPuertas", false);
                    //Retraso de 3 seg para bajar.
                    yield return new WaitForSeconds(3);
                    elevator2.SetBool("subir", false);
                }
                break;
        }
    }
}
