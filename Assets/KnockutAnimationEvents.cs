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

    public IEnumerator Finish()
    {
        while(UIManager.Instance._knockImage[1].fillAmount != 1)
        {
            yield return new WaitForEndOfFrame();
        }
        GameManager.Instance.LoadEScene("Prision");
    }

}
