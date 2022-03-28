using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private Animator button1 = null;
    [SerializeField] private Animator button2 = null;
    [SerializeField] private Animator button3 = null;
    [SerializeField] private Animator button4 = null;
    [SerializeField] private Animator button5 = null;
    [SerializeField] private Animator elevator = null;

    public IEnumerator OnPress(GameObject button)
    {
        switch(button.tag){
            case "upButton":
                Debug.Log("Bajar ascensor");
                button1.SetBool("pressed", true);
                button5.SetBool("pressed", true);
                elevator.SetBool("subir", false);
            break;
            case "downButton":
                Debug.Log("Subir ascensor");
                button2.SetBool("pressed", true);
                button3.SetBool("pressed", true);
                button4.SetBool("pressed", true);
                elevator.SetBool("subir", true);
            break;
        }
        yield return new WaitForSeconds(1);
        button1.SetBool("pressed", false);
        button2.SetBool("pressed", false);
        button3.SetBool("pressed", false);
        button4.SetBool("pressed", false);
        button5.SetBool("pressed", false);
    }
}
