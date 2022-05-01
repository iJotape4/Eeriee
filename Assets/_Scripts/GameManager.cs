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
    bool _isGameOver = false;
    public bool IsGameOver { get => _isGameOver; }
    bool _isPaused = false;

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
        DontDestroyOnLoad(this);
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

    public void PlayerDead()
    {
        if (_healthCount <= 0)
        {
            UIManager.Instance.ShowGameOver();
        }
    }

    public void updateHealth(float amount)
    {
        _healthCount += amount;
    }
}
