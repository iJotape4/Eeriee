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

    #endregion

    #region Player
    FirstPersonController _player = new FirstPersonController();
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
}
