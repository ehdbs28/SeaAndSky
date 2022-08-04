using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGoal : MonoBehaviour
{
    public static bool isGoal = false;

    private bool isLoad;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !isLoad)
        {
            //isGoal = true;
            isLoad = true;
            PlayerGoalIN();
        }
    }

    void PlayerGoalIN()
    {
        Debug.Log("Goal");
        int curStage = ++DataManager.Instance.User.stage;

        if (DataManager.Instance.User.maxStage < curStage)
            DataManager.Instance.User.maxStage = curStage;

        SceneManager.LoadScene("Main");
    }
}
