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
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private TextMeshProUGUI _fishText;

    [SerializeField]
    private CanvasGroup esc;
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private GameObject goal;
    [SerializeField]
    private CanvasGroup keyPanel;

    [SerializeField] private CanvasGroup AudioSetting;

    [SerializeField]
    private CanvasGroup interactionButton;

    private void Start() {
        _hpText.text = $"- {DataManager.Instance.User.playerDie}";
        _fishText.text = $"+ {DataManager.Instance.User.playerFishScore}";
    }

    void Update()
    {
        _fishText.text = $"+ {DataManager.Instance.User.playerFishScore}";

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.GameState == GameState.InGame && !esc.gameObject.activeSelf)
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
                if(GameManager.Instance.GameState == GameState.Setting){
                    if (keyPanel.gameObject.activeSelf)
                        KeySetQuit();
                    else if (AudioSetting.gameObject.activeSelf)
                        AudioSettingQuit();
                    else
                        UnshowEscPanel();
                }
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

    private IEnumerator ReduceHpCoroutine(Transform playerTrm, Vector2 cheakPoint, Action Reset = null){
        GameManager.Instance.IsInvincibility = true;
        DataManager.Instance.User.playerDie++;
        _hpText.text = $"- {DataManager.Instance.User.playerDie}";

        PlayerRevival(playerTrm, cheakPoint);

        Reset?.Invoke();    

        yield return new WaitForSecondsRealtime(0.5f);

        GameManager.Instance.IsInvincibility = false;
    }

    private void PlayerRevival(Transform playerTrm, Vector2 cheakPoint)
    {
        GameManager.Instance.PlayerState = cheakPoint.y > 0 ? AreaState.Sky : AreaState.Sea;
        FindObjectOfType<PlayerArea>().ChangedState(); 
        if(playerTrm.parent != null) playerTrm.SetParent(null);
        playerTrm.position = cheakPoint;
    }

    public void ContinueBtn()
    {
        UnshowEscPanel();
    }

    public void ReStart()
    {
        Time.timeScale = 1f;
        UnshowEscPanel();
        SceneChangeManager.Instance.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void KeySetBtn()
    {
        keyPanel.gameObject.SetActive(true);
        keyPanel.DOFade(1f, 0.5f).SetUpdate(true);
    }
    public void KeySetQuit()
    {
        keyPanel.DOFade(0f, 0.5f).OnComplete(() => keyPanel.gameObject.SetActive(false)).SetUpdate(true);
    }


    public void BtnClick(GameObject g)
    {
        g.SetActive(false);
    }

    private void UnshowEscPanel()
    {
        esc.DOFade(0f, 0.3f).OnComplete(() => esc.gameObject.SetActive(false));
        GameManager.Instance.GameState = GameState.InGame;
        Time.timeScale = 1f;
    }

    public void AudioSettingBtn()
    {
        AudioSetting.gameObject.SetActive(true);
        AudioSetting.DOFade(1f, 0.5f).SetUpdate(true);
    }

    public void AudioSettingQuit()
    {
        AudioSetting.DOFade(0f, 0.5f).OnComplete(() => AudioSetting.gameObject.SetActive(false)).SetUpdate(true);
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