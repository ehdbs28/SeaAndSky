using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [Header("About Player Hp")]
    [SerializeField] private int _hpCount = 3;
    [SerializeField] private int _currentHp;
    [SerializeField] private TextMeshProUGUI _hpText;

    [SerializeField]
    private CanvasGroup esc;
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private GameObject goal;
    [SerializeField]
    private GameObject keyPanel;

    [SerializeField] private GameObject AudioSetting;

    [SerializeField]
    private CanvasGroup interactionButton;

    private void Start() {
        _currentHp = _hpCount;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ���� ���� ���� �� => �����ִ� ����
            if (!esc.gameObject.activeSelf)
            {
                esc.gameObject.SetActive(true);
                esc.DOFade(1f, 0.5f).OnComplete(() =>
                {
                    GameManager.Instance.GameState = GameState.Setting;
                    Time.timeScale = 0f;
                });
            }
            else
            {
                if (keyPanel.gameObject.activeSelf)
                    KeySetQuit();
                else if (AudioSetting.gameObject.activeSelf)
                    AudioSettingQuit();
                else
                    UnshowEscPanel();
            }
        }

        if (GameManager.Instance.IsPlayerDeath)
        {
            gameOverUI.SetActive(true);
            GameManager.Instance.IsPlayerDeath = false;
        }
    }

    public void ReduceHeart(Transform playerTrm ,Vector2 cheakPoint, Action OnPlayerDead = null){
        if(GameManager.Instance.IsPlayerDeath) return;

        if(!GameManager.Instance.IsInvincibility){
            StartCoroutine(ReduceHpCoroutine(playerTrm, cheakPoint, OnPlayerDead));
        }
    }

    private IEnumerator ReduceHpCoroutine(Transform playerTrm, Vector2 cheakPoint, Action OnPlayerDead = null){
        GameManager.Instance.IsInvincibility = true;
        _currentHp--;
        _hpText.text = $"X {_currentHp}";

        if(_currentHp == 0){
            GameManager.Instance.IsGameOver = true;
            GameManager.Instance.IsPlayerDeath = true;
            OnPlayerDead?.Invoke();
        }
        else PlayerRevival(playerTrm, cheakPoint);

        yield return new WaitForSecondsRealtime(0.1f);

        GameManager.Instance.IsInvincibility = false;
    }

    private void PlayerRevival(Transform playerTrm, Vector2 cheakPoint)
    {
        GameManager.Instance.PlayerState = cheakPoint.y > 0 ? AreaState.Sky : AreaState.Sea;
        FindObjectOfType<PlayerArea>().ChangedState();
        playerTrm.position = cheakPoint;
    }

    public void ContinueBtn()
    {
        UnshowEscPanel();
    }

    public void NextStage()
    {
        Debug.Log("���� ��������");
    }

    public void ReStart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void KeySetBtn()
    {
        keyPanel.SetActive(true);
    }
    public void KeySetQuit()
    {
        keyPanel.SetActive(false);
    }

    private void UnshowEscPanel()
    {
        esc.DOFade(0f, 0.3f).OnComplete(() => esc.gameObject.SetActive(false));
        GameManager.Instance.GameState = GameState.InGame;
        Time.timeScale = 1f;
    }

    public void AudioSettingBtn()
    {
        AudioSetting.SetActive(true);
    }
    public void AudioSettingQuit()
    {
        AudioSetting.SetActive(false);
    }

    public void SetInteractionButton(bool isActive, Vector2 pos = default)
    {
        if (isActive)
        {
            interactionButton.DOKill();
            interactionButton.DOFade(1f, 1f);

            interactionButton.transform.position = pos;
        }
        else
        {
            interactionButton.DOKill();
            interactionButton.DOFade(0f, 0.3f);
        }
    }
}