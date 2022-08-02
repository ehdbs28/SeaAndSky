using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

public class GameManager : MonoSingleton<GameManager>
{
    #region 플레이어 HP 관련 코드
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
    public Vector2 PlayerPosition { get => currentStage.transform.GetChild(0).position; }
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
    #endregion

    [field: SerializeField]
    public Camera skyCamera { get; private set; }

    [field: SerializeField]
    public Camera seaCamera { get; private set; }

    private void Awake()
    {
        UIManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        for (int i = 0; i < _heartCnt; i++)
        {
            GameObject heart = Instantiate(_heartPrefab);
            heart.transform.SetParent(_parentTrm);
            heartList.Add(heart);
        }

        LoadStage();
    }

    void Update()
    {
        GameReset();
    }

    //플레이어 하트 감소
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

            sq.Append(lastIndex.transform.DOScale(new Vector3(2.2f, 2.2f, 2.2f), 0.2f));
            sq.Append(lastIndex.transform.DOScale(new Vector3(0, 0, 0), 0.5f));
            sq.OnComplete(() =>
            {
                Destroy(lastIndex);
                _isInvincibility = false;
            });

            heartList.RemoveAt(heartList.Count - 1);

            if(heartList.Count == 0)
            {
                _isplayerDeath = true;
                _isGameOver = true;

                OnPlayerDead?.Invoke();
            }
        }
    }

    private void PlayerRevival(Transform playerTrm, Vector2 cheakPoint)
    {
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
            Debug.Log($"Stage {stage}");
            currentStage = Instantiate(stages.stages[stage - 1], Vector3.zero, Quaternion.identity);

            EventManager.TriggerEvent("LoadStage");
        }
    }
}
