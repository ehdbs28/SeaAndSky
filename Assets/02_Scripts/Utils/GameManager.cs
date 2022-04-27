using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public int playerJumpCount = 0;


    void Start()
    {
        playerJumpCount = 1;
    }
}
