using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;


    #region Inspector Properties
    [SerializeField] Healthbar _healthbar;
    [SerializeField] GameObject _pausePanel;
    [SerializeField] GameObject _dmgImage;
    #endregion
    private void Awake()
    {
        if (UIManager.Instance == null)
        {
            UIManager.Instance = this.GetComponent<UIManager>();

        }
        else if (UIManager.Instance != null && UIManager.Instance != this)
        {
            Destroy(gameObject);
            return;

        }
        DontDestroyOnLoad(this);

    

    }

    private void Start()
    {
        _pausePanel = GameObject.Find("PausePanel"); _pausePanel.SetActive(false);
        _healthbar = GameObject.Find("HealthBar").GetComponentInChildren<Healthbar>();
        _dmgImage = GameObject.Find("DmgFlashImage"); _dmgImage.SetActive(false);

    }
    /*  public void ShowGameOver()
      {
          gameOverText.gameObject.SetActive(true);
          playAgainButton.gameObject.SetActive(true);
      }

      // Update is called once per frame
      void Update()
      {
          if (Input.GetButtonDown("Pause"))
              Pause();
      }

      public void UpdateLives(int lives)
      {

          if (lives < 0)
          {
              livesText.text = "";
              return;
          }
          livesText.text = "x" + lives;
          Cora1.enabled = true;
          Cora2.enabled = true;
      } */

    public void UpdateHealth(float damage)
    {
        _healthbar.LooseHealth(damage);
        StartCoroutine("DmgFlash");

    }

    public IEnumerator DmgFlash()
    {
        _dmgImage.SetActive(true);
      yield return new WaitForSeconds(0.08f);
        _dmgImage.SetActive(false);
    }

     public void Pause()
     {
         if (_pausePanel.activeSelf)
             _pausePanel.SetActive(false);
         else
             _pausePanel.SetActive(true);

         Time.timeScale = (_pausePanel.activeSelf) ? 0 : 1;
     }
    /*
     public void ActivateTutIcon(string IconName)
     {

         TutIcon1.gameObject.SetActive(true);
     }

     public void DesactivateTutIcon(string IconName)
     {

         TutIcon1.gameObject.SetActive(false);
     }
 }*/
}
