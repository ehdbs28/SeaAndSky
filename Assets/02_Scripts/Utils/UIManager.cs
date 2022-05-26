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


    private PlayerMove player;
    private void Start()
    {
        player = FindObjectOfType<PlayerMove>();
    }
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

        if(player.isDeath)
        {
            gameOver.SetActive(true);
            player.isDeath = false;
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
