using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] // 테스트용 빌드시 세리얼라이즈필드 제거
    private WorldState _state;
    public WorldState _State
    {
        get => _state;
        set
        {
            _state = value;
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

    void Update()
    {
        GameReset();
    }

    void GameReset()
    {
        if(Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
