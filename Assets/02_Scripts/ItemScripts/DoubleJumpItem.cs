using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ConstantManager;

public class DoubleJumpItem : MonoBehaviour
{

    private void Start()
    {
        gameObject.SetActive(true);  
        EventManager.StartListening(DOUBLE_JUMP, DoubleJump);
    }

    void DoubleJump()
    {
        Debug.Log("double");
        GameManager.Instance.playerJumpCount = 2;
        gameObject.SetActive(false);
    }
}
