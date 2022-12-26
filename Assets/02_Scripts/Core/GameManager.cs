using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

public class GameManager : MonoSingleton<GameManager>
{
    #region 
    private bool _isplayerDeath = false;
    public bool IsPlayerDeath
    {
        set => _isplayerDeath = value;
        get => _isplayerDeath;
    }
    private bool _isGameOver = false;
    public bool IsGameOver
    {
        get => _isGameOver; set => _isGameOver = value;
    }
    private bool _isInvincibility = false;
    public bool IsInvincibility {get => _isInvincibility; set => _isInvincibility = value;}
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
    public TimeManager timeManager {get; private set;}
    public TileManager TileManager {get; private set;}
    #endregion

    [field: SerializeField]
    public Camera skyCamera { get; private set; }
    public StateCamera skyCamState => skyCamera.GetComponent<StateCamera>();

    [field: SerializeField]
    public Camera seaCamera { get; private set; }
    public StateCamera seaCamState => seaCamera.GetComponent<StateCamera>();

    public Camera CurrentCam{
        get{
            if(skyCamState.IsMainCam) return skyCamera;
            else return seaCamera;
        }
    }

    private GameState gameState;
    public GameState GameState { get => gameState; set => gameState = value; }

    private PlayerAudio playerAudio;

    private void Awake()
    {
        SoundManager.Instance.SoundManagerAwake();

        timeManager = FindObjectOfType<TimeManager>();
        UIManager = FindObjectOfType<UIManager>();
        playerAudio = FindObjectOfType<PlayerAudio>();
        TileManager = FindObjectOfType<TileManager>();

        LoadStage();
    }

    void Update()
    {
        GameReset();
    }

    void GameReset()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UIManager.ReStart();
        }
    }

    private void LoadStage()
    {
        if (isLoadState)
        {
            int stage = DataManager.Instance.User.stage;

            if(stage > stages.stages.Count)
            {
                SceneChangeManager.Instance.LoadScene("MTitle");
                DataManager.Instance.User.stage = 1;
                return;
            }
            currentStage = Instantiate(stages.stages[stage - 1], Vector3.zero, Quaternion.identity);

            EventManager.TriggerEvent("LoadStage");
            SoundManager.Instance.PlayRandomBGM();
        }
    }
}
