using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    #region 싱글톤
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if(instance == null)
                {
                    GameObject gameManager = new GameObject("GameManager");
                    gameManager.AddComponent<GameManager>();
                    instance = gameManager.GetComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    #endregion

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
    #endregion

    private AreaState _playerState = AreaState.Sky;
    public AreaState PlayerState
    {
        get => _playerState;
        set
        {
            _playerState = value;
        }
    }
    private void Awake()
    {
        if(Instance != this)
        {
            Destroy(Instance.gameObject);
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        for (int i = 0; i < _heartCnt; i++)
        {
            GameObject heart = Instantiate(_heartPrefab);
            heart.transform.SetParent(_parentTrm);
            heartList.Add(heart);
        }
    }

    void Update()
    {
        GameReset();
    }

    //플레이어 하트 감소
    public void ReduceHeart()
    {
        if (heartList.Count > 0)
        {
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
        else
        {
            _isplayerDeath = true;
        }
    }

    void GameReset()
    {
        if(Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
