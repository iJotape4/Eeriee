using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    #region Private Properties

    [SerializeField] int _livesCount = 2;
    [SerializeField] int _energyCount = 100;
    [SerializeField] int _manaCount = 100;
    [SerializeField] float _healthCount = 100f;
    [SerializeField] int _holyWaterCount = 10;
    [SerializeField] int _saltCount = 10;

    // int _scoreCount = 0;
    //int _currentLevel = 0;
    [SerializeField] bool _isGameOver = false;
    public bool IsGameOver { get => _isGameOver; }
    [SerializeField] bool _isPaused = false;
    public bool Ispaused { get => _isPaused; }
    [SerializeField] bool _EerieObtained = false;

    #endregion

    #region FlowGame Events
    public bool EerieObtained { get => _EerieObtained; }

    [SerializeField] bool _inSubBossFight = false;
    public bool InSubBossFight { get => _inSubBossFight; }

    [SerializeField] bool _yellowCard = false;
    public bool YellowCard { get => _yellowCard; }

    [SerializeField] bool _pinkCard = false;
    public bool PinkCard { get => _pinkCard; }

    [SerializeField] int _actualEvent = 0;
    public int ActualEvent { get => _actualEvent; }


    #endregion

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            GameManager.Instance = this.GetComponent<GameManager>();
        }
        else if (GameManager.Instance != null && GameManager.Instance != this)
        {
            Destroy(gameObject);
            return;
        }
       // DontDestroyOnLoad(this);
    }


    // Start is called before the first frame update
    void Start()
    {
        InteractableObject._eventsList[_actualEvent].enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDead();
    }

    public void PauseGame(bool isPaused)
    {
        _isPaused = (isPaused) ? false : true;
        Time.timeScale = (_isPaused) ? 0 : 1;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void PlayerDead()
    {
        if (_healthCount <= 0)
        {
            _isGameOver = true;
            UIManager.Instance.ShowGameOver();
            Cursor.lockState = CursorLockMode.None;         
        }
    }


    public void updateHealth(float amount)
    {
        _healthCount += amount;
    }

    public void eerieObtention()
    {
        _EerieObtained = true;
    }

    public void SubBossFight(bool active)
    {
        _inSubBossFight = active;
    }

    public void YellowCardObtention(bool active)
    {
        _yellowCard = active;
    }

    public void PinkCardObtention(bool active)
    {
        _pinkCard = active;
    }


   public void NextEvent()
    {
        _actualEvent +=1;

        InteractableObject._eventsList[_actualEvent].enabled = true;
        InteractableObject._crossMark.transform.parent = InteractableObject._eventsList[_actualEvent].transform;
        InteractableObject._eventsList[_actualEvent].GetComponent<BoxCollider>().enabled = true;
        InteractableObject._crossMark.transform.localPosition = (new Vector3(0f, 9f, 0f));
    }
}
