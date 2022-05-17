using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private GameManager _gameManager;

    #region Inspector Properties
    [SerializeField] Healthbar _healthbar;
    [SerializeField] GameObject _pausePanel;
    [SerializeField] GameObject _dmgImage;

    [SerializeField] GameObject _gameOverPanel;

    StarterAssets.StarterAssetsInputs _input;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
    [SerializeField] PlayerInput _playerInput;
#endif

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
      //  DontDestroyOnLoad(this);

    

    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _pausePanel = GameObject.Find("PausePanel"); _pausePanel.SetActive(false);
        _healthbar = GameObject.Find("HealthBar").GetComponentInChildren<Healthbar>();
        _dmgImage = GameObject.Find("DmgFlashImage"); _dmgImage.SetActive(false);
        _gameOverPanel = GameObject.Find("GameOverPanel"); _gameOverPanel.gameObject.SetActive(false);
      

    }
    public void ShowGameOver()
      {
          _gameOverPanel.gameObject.SetActive(true);
        
      }

      // Update is called once per frame
      void Update()
      {
       
      }

    public void UpdateHealth(float damage)
    {
        GameManager.Instance.updateHealth(damage);    
        _healthbar.updateHealthBar(damage);
        StartCoroutine("DmgFlash");

    }

    public IEnumerator DmgFlash()
    {
        _dmgImage.SetActive(true);
      yield return new WaitForSeconds(0.08f);
        _dmgImage.SetActive(false);
    }

     public  void Pause()
     {
        _pausePanel.SetActive(_pausePanel.activeSelf ? false : true);
        GameManager.Instance.PauseGame(_pausePanel.activeSelf);
        Time.timeScale = (_pausePanel.activeSelf) ? 0 : 1;
     }

    public void SmartWatch()
    {
        _pausePanel.SetActive(_pausePanel.activeSelf ? false : true);
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
