using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

public class GameManager : MonoSingleton<GameManager>
{
    #region 
    [SerializeField] private int _heartCnt;
    [SerializeField] private GameObject _heartPrefab;
    [SerializeField] private Transform _parentTrm;
    
    private List<GameObject> heartList = new List<GameObject>();

    private bool _isplayerDeath = false;
    public bool IsPlayerDeath
    {
        set => _isplayerDeath = value;
        get => _isplayerDeath;
    }
    private bool _isGameOver = false;
    public bool IsGameOver
    {
        get => _isGameOver;
    }
    private bool _isInvincibility = false;
    #endregion
    #region Stage
    [SerializeField] StageSO stages;
    private GameObject currentStage;
    public Vector2 PlayerPosition 
    {
        get 
        {
            if (isLoadState)
                return currentStage.transform.GetChild(0).position;
            else
                return new Vector2(-93.5f, 15);
        } 
    }
    #endregion

    public bool isLoadState = false;

    private AreaState _playerState = AreaState.Sky;
    public AreaState PlayerState
    {
        get => _playerState;
        set
        {
            _playerState = value;
        }
    }

    #region Controller
    public UIManager UIManager { get; private set; }
    public TimeManager timeManager {get; private set;}
    #endregion

    [field: SerializeField]
    public Camera skyCamera { get; private set; }

    [field: SerializeField]
    public Camera seaCamera { get; private set; }

    private GameState gameState;
    public GameState GameState { get => gameState; set => gameState = value; }

    private PlayerAudio playerAudio;

    private void Awake()
    {
        timeManager = FindObjectOfType<TimeManager>();
        UIManager = FindObjectOfType<UIManager>();
        playerAudio = FindObjectOfType<PlayerAudio>();
        
        for (int i = 0; i < _heartCnt; i++)
        {
            GameObject heart = Instantiate(_heartPrefab);
            heart.transform.SetParent(_parentTrm);
            heart.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            heartList.Add(heart);
        }

        LoadStage();
    }

    void Update()
    {
        GameReset();
    }

    public void ReduceHeart(Transform playerTrm ,Vector2 cheakPoint, Action OnPlayerDead = null)
    {
        if (_isplayerDeath) return;

        if (heartList.Count > 0 && !_isInvincibility)
        {
            _isInvincibility = true;

            if(heartList.Count != 0)
            {
                PlayerRevival(playerTrm, cheakPoint);
            }

            GameObject lastIndex = heartList[heartList.Count - 1];
            Sequence sq = DOTween.Sequence();
            sq.Append(lastIndex.transform.DOScale(Vector3.one * 2.2f, 0.2f));
            sq.Append(lastIndex.transform.DOScale(Vector3.zero, 0.5f));
            sq.OnComplete(() =>
            {
                Destroy(lastIndex);
                _isInvincibility = false;
            });

            heartList.RemoveAt(heartList.Count - 1);

            playerAudio.PlayerDieSound();

            if (heartList.Count == 0)
            {
                _isplayerDeath = true;
                _isGameOver = true;

                OnPlayerDead?.Invoke();
            }
        }
    }

    private void PlayerRevival(Transform playerTrm, Vector2 cheakPoint)
    {
        _playerState = cheakPoint.y > 0 ? AreaState.Sky : AreaState.Sea;
        FindObjectOfType<PlayerArea>().ChangedState();
        playerTrm.position = cheakPoint;
    }

    void GameReset()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void LoadStage()
    {
        if (isLoadState)
        {
            int stage = DataManager.Instance.User.stage;

            if(stage > stages.stages.Count)
            {
                SceneManager.LoadScene("MTitle");
                DataManager.Instance.User.stage = 1;
                return;
            }
            currentStage = Instantiate(stages.stages[stage - 1], Vector3.zero, Quaternion.identity);

            EventManager.TriggerEvent("LoadStage");
        }
    }
}
