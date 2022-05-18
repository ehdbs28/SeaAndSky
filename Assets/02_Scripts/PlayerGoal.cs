using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ConstantManager;

public class PlayerGoal : MonoBehaviour
{
    void Start()
    {
        EventManager.StartListening(GOAL, PlayerGoalIN);
    }

    void PlayerGoalIN()
    {
        Time.timeScale = 0;
        Debug.Log("GOAL!");
    }
}
