using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private GameManager _gameManager;
    public Animator _smartWatchAnim;
    public string _animEnableBool;

    public Image _nextButton;
    public Image _holdNextButton;
    #region Inspector Properties
    private Image _smartWatch;

    private Image _eUIcon;
    private Sprite _eUIconAllow;
    private Sprite _eUIconForbbidden;

    [SerializeField] private Image _blueEye;
    [SerializeField] private Sprite _blueEyeAllow;
    [SerializeField] private Sprite _blueEyeForbbidden;

    public TextMeshProUGUI _textinScreen;
    
    [SerializeField] Healthbar _healthbar;
    public GameObject _bossHealthBar;
    [SerializeField] GameObject _pausePanel;
    [SerializeField] GameObject _dmgImage;
    [SerializeField] Image[] _knockImage;

    [SerializeField] GameObject _gameOverPanel;
    public Image[] _Uicons;
    [SerializeField] GameObject _selectionCircle;
    public GameObject _thanks4PlayingPanel;

    StarterAssets.StarterAssetsInputs _input;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED

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
        _bossHealthBar = FindObjectOfType<BossHealthBar>().gameObject.transform.parent.gameObject; 
        _bossHealthBar.SetActive(false);
        _dmgImage = GameObject.Find("DmgFlashImage"); _dmgImage.SetActive(false);
        _gameOverPanel = GameObject.Find("GameOverPanel"); _gameOverPanel.gameObject.SetActive(false);
        _smartWatchAnim = _pausePanel.GetComponent<Animator>();
        _Uicons = GameObject.Find("UiCons").gameObject.transform.GetComponentsInChildren<Image>();
        _selectionCircle = GameObject.Find("SelectionCircle");

        _smartWatch = GameObject.Find("UiconSmartWatch").GetComponent<Image>();
        _smartWatch.enabled = false;

        _eUIcon = GameObject.Find("eUIcon").GetComponent<Image>();
        _eUIcon.enabled = false;
        _eUIconAllow = Resources.Load<Sprite>("Sprites/EInteract");
        _eUIconForbbidden = Resources.Load<Sprite>("Sprites/Eblocked");

        _blueEye = GameObject.Find("UiconEye").GetComponent<Image>();
        _blueEye.enabled = false;
        _blueEyeAllow = Resources.Load<Sprite>("Sprites/BlueEye");
        _blueEyeForbbidden = Resources.Load<Sprite>("Sprites/BlueEyeBlocked");

        _thanks4PlayingPanel = GameObject.Find("Thanks4PlayingPanel");
        _thanks4PlayingPanel.SetActive(false);

        _knockImage = GameObject.Find("KnockImages").GetComponentsInChildren<Image>();

        AwakePlayer();
        UiConsStart();

    }
    public void ShowGameOver()
    {
        _gameOverPanel.gameObject.SetActive(true);

    }

    public void BlueEyeActivation()
    {
        _blueEye.enabled = true;
    }

    public void SwitchBlueEye()
    {
        _blueEye.sprite = (_blueEye.sprite == _blueEyeAllow? _blueEyeForbbidden: _blueEyeAllow);
    }


    public void SmartWatchUIconActivation()
    {
        _smartWatch.enabled = true;
    }

    public void NextButtonActivation(bool activation)
    {
        _nextButton.enabled = activation;
        _holdNextButton.enabled = activation;
    }

    public void ShowInteractionAllowed()
    {
        _eUIcon.enabled = true;
        _eUIcon.sprite = _eUIconAllow;
    } 
    
    public void ShowInteractionForbbidden()
    {
        _eUIcon.enabled = true;
        _eUIcon.sprite = _eUIconForbbidden;
    }

    public void DisableInteraction()
    {
        _eUIcon.enabled = false;
    }


    public void UiConsStart()
    {

        activateUiCon(0);
        foreach(Image uicon in _Uicons)
        {
            uicon.enabled = false;
            uicon.transform.parent.gameObject.SetActive(false);
        }
        
        
    }

    public void activateUiCon(int index)
    {    
        _Uicons[index].GetComponent<RectTransform>().SetAsLastSibling();
        _selectionCircle.transform.parent = _Uicons[index].transform;
        _selectionCircle.transform.localPosition = new Vector3(0f, 0f, 0f);
    }
    public void UpdateHealth(float damage)
    {
        GameManager.Instance.updateHealth(damage);
        _healthbar.updateHealthBar(damage);
        StartCoroutine("DmgFlash");

    }

    public void BossHealthBarActivation()
    {
        _bossHealthBar.SetActive(_gameManager.InSubBossFight ? true : false);
    }

    public Image Hacking()
    {

        return _bossHealthBar.transform.GetChild(0).GetComponent<Image>();
     
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
        Cursor.lockState = (Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.Confined : CursorLockMode.Locked) ;
    }

    public void KnockOutAnimation()
    {
        foreach (Image parpad in _knockImage)
        {
            StartCoroutine(parpadsAnimation(parpad));
        }    
    }

    public IEnumerator parpadsAnimation(Image parpad)
    {
        parpad.fillAmount = 1f;
        parpad.enabled = true;

        while (parpad.fillAmount >= 0.4f)
        {
            parpad.fillAmount -= 0.005f;
            yield return new WaitForEndOfFrame();
        }

        while (parpad.fillAmount != 1f)
        {
            parpad.fillAmount += 0.005f;
            yield return new WaitForEndOfFrame();
        }

        while (parpad.fillAmount >= 0.6f)
        {
            parpad.fillAmount -= 0.005f;
            yield return new WaitForEndOfFrame();
        }

        while (parpad.fillAmount != 1f)
        {
            parpad.fillAmount += 0.005f;
            yield return new WaitForEndOfFrame();
        }

        while (parpad.fillAmount >= 0.8f)
        {
            parpad.fillAmount -= 0.005f;
            yield return new WaitForEndOfFrame();
        }

        while (parpad.fillAmount != 1f)
        {
            parpad.fillAmount += 0.005f;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }


    public void AwakePlayer()
    {
        foreach (Image parpad in _knockImage)
        {
            StartCoroutine(ParpadsAwake(parpad));
        }
    }

    public IEnumerator ParpadsAwake(Image parpad)
    {
        parpad.enabled = true;

        while (parpad.fillAmount != 0)
        {
            parpad.fillAmount -= 0.005f;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

}
