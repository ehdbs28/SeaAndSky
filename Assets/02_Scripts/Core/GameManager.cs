using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

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
    #endregion

    #region Stage
    [SerializeField] private List<GameObject> stages;
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

        //test
        if (Input.GetKeyDown(KeyCode.L))
        {
            ReduceHeart();
        }
    }

    //플레이어 하트 감소
    public void ReduceHeart()
    {
        if (_isplayerDeath) return;
        if (heartList.Count > 0)
        {
            _isplayerDeath = true;
            GameObject lastIndex = heartList[heartList.Count - 1];
            Sequence sq = DOTween.Sequence();

            sq.Append(lastIndex.transform.DOScale(new Vector3(2.2f, 2.2f, 2.2f), 0.2f));
            sq.Append(lastIndex.transform.DOScale(new Vector3(0, 0, 0), 0.5f));
            sq.OnComplete(() =>
            {
                Destroy(lastIndex);
            });

            heartList.RemoveAt(heartList.Count - 1);
        }
        else if (heartList.Count == 0)
        {
            _isGameOver = true;
        }
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
            currentStage = Instantiate(stages[stage - 1], Vector3.up * 11f, Quaternion.identity);

            EventManager.TriggerEvent("LoadStage");
        }
    }
}
