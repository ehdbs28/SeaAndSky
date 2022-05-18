using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region ΩÃ±€≈Ê
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
    private AreaState _playerState;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
