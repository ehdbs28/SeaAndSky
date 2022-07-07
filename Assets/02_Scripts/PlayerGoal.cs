using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoal : MonoBehaviour
{
    public static bool isGoal = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerGoalIN();
            isGoal = true;
        }
    }

    void PlayerGoalIN()
    {
        Time.timeScale = 0;
    }
}
