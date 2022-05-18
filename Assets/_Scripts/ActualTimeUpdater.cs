using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActualTimeUpdater : MonoBehaviour
{
    public TextMeshProUGUI hours;
    public TextMeshProUGUI minutes;
    public TextMeshProUGUI dots;

    public int hour;
    public int minute;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateTime());
        StartCoroutine(FlashDots());
    }


    public IEnumerator UpdateTime()
    {
        hour = System.DateTime.Now.Hour;
        minute = System.DateTime.Now.Minute;
        hours.text = "" + hour;
        minutes.text = "" + minute;
        yield return new WaitForSecondsRealtime(60f);
        StartCoroutine(UpdateTime());
    }

    public IEnumerator FlashDots()
    {
        while (!GameManager.Instance.IsGameOver)
        {
            yield return new WaitForSeconds(0.3f);
             dots.color = new Color32(255,255,255,0) ;
            
           
        }
    }
}
