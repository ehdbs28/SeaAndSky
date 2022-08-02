using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private int escMenu = 0;

    [SerializeField]
    private GameObject esc;
    [SerializeField]
    private GameObject gameOver;
    [SerializeField]
    private GameObject goal;
    [SerializeField]
    private GameObject keyPanel;

    [SerializeField]
    private CanvasGroup interactionButton;

    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (escMenu % 2 == 0)
            {
                esc.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                esc.SetActive(false);
                Time.timeScale = 1;
            }
            escMenu++;
        }

        if (GameManager.Instance.IsPlayerDeath)
        {
            gameOver.SetActive(true);
            GameManager.Instance.IsPlayerDeath = false;
        }
        if (PlayerGoal.isGoal)
        {
            goal.SetActive(true);
            PlayerGoal.isGoal = false;
        }
    }

    public void ContinueBtn()
    {
        esc.SetActive(false);
        Time.timeScale = 1f;
    }

    public void NextStage()
    {
        Debug.Log("다음 스테이지");
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
