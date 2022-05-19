using System.Collections;
using System.Collections.Generic;
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (escMenu % 2 == 0)
            {
                esc.SetActive(true);
            }
            else
            {
                esc.SetActive(false);
            }
            escMenu++;
        }

        if(PlayerMove.isDeath)
        {
            gameOver.SetActive(true);
            PlayerMove.isDeath = false;
        }

        if(PlayerGoal.isGoal)
        {
            goal.SetActive(true);
            PlayerGoal.isGoal = false;
        }
    }

    public void NextStage()
    {
        Debug.Log("다음 스테이지");
    }

    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }

}
