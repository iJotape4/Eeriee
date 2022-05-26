using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockutAnimationEvents : MonoBehaviour
{
    public void DamageFlash()
    {
        StartCoroutine(UIManager.Instance.DmgFlash());
    }

    public void KnockOutAnimation()
    {
        UIManager.Instance.KnockOutAnimation();
        
    }
    public void activateSound()
    {
        GetComponent<AudioSource>().enabled = true;
    }

    public void Finish()
    {
        GameManager.Instance.LoadEScene("Jpruebas 2.0");
    }

}
