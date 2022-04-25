using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;


    #region Inspector Properties
    [SerializeField] Healthbar healthbar;
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

       // pausePanel = GameObject.Find("PausePanel"); pausePanel.SetActive(false);

    }

    private void Start()
    {
        healthbar = GameObject.Find("HealthBar").GetComponentInChildren<Healthbar>();
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
        healthbar.LooseHealth(damage); 

    }

    /* public void Pause()
     {
         if (pausePanel.activeSelf)
             pausePanel.SetActive(false);
         else
             pausePanel.SetActive(true);

         Time.timeScale = (pausePanel.activeSelf) ? 0 : 1;
     }

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
