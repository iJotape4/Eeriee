using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    #region Inspector Properties
    [Header ("Important Systems")]
    private GameManager _gameManager;

    [Header("Animator System")]
    public Animator _smartWatchAnim;
    public string _animEnableBool;

    [Header("Dialogues Control")]
    public TextMeshProUGUI _textinScreen;

    public Image _nextButton;
    public Image _holdNextButton;

    [Header("Paneles")]
    public GameObject _smartWatchPanel;
    public GameObject _gameOverPanel;
    public GameObject _thanks4PlayingPanel;

    [Header("UIcon")]
    public Image _eUIcon;
    [SerializeField] private Sprite _eUIconAllow;
    [SerializeField] private Sprite _eUIconForbbidden;

    public Image _blueEye;
    [SerializeField] private Sprite _blueEyeAllow;
    [SerializeField] private Sprite _blueEyeForbbidden;

    public Image _smartWatch;

    public GameObject _selectionCircle;
    public Image[] _Uicons;

    [Header("HealthBars")]
    public Healthbar _healthbar;
    public GameObject _bossHealthBar;

    [Header("KnockOut")]
    [SerializeField] GameObject _dmgImage;
    [SerializeField] Image[] _knockImage;

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
        _smartWatchAnim = _smartWatchPanel.GetComponent<Animator>();
        _thanks4PlayingPanel.SetActive(false);
        _gameOverPanel.gameObject.SetActive(false);

        _bossHealthBar.SetActive(false);
        _dmgImage.SetActive(false);       
        _smartWatch.enabled = false;
        _eUIcon.enabled = false;
        _blueEye.enabled = false;

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
        _smartWatchPanel.SetActive(_smartWatchPanel.activeSelf ? false : true);
        GameManager.Instance.PauseGame(_smartWatchPanel.activeSelf);
        Time.timeScale = (_smartWatchPanel.activeSelf) ? 0 : 1;
     }

    public void SmartWatch()
    {
        _smartWatchAnim.SetBool("Enable", _smartWatchAnim.GetBool("Enable") ? false : true);
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
