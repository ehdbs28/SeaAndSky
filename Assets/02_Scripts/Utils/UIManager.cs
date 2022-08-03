using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
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
                UnshowEscPanel();
            }
        }

        if (GameManager.Instance.IsPlayerDeath)
        {
            gameOverUI.SetActive(true);
            GameManager.Instance.IsPlayerDeath = false;
        }
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