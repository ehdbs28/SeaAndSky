using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArea : MonoBehaviour
{
    private PlayerMove _playerMove;
    private void Start()
    {
        _playerMove = GetComponent<PlayerMove>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SetStateChanged();
        }
        //CheckState();
    }

    //private void CheckState()
    //{
    //    if (GameManager.Instance.PlayerState == AreaState.Sky)
    //    {
     
    //    }
    //    else if (GameManager.Instance.PlayerState == AreaState.Sea)
    //    {
           
    //    }
    //}
    
    public void ChangedState()
    {
        if (GameManager.Instance.PlayerState == AreaState.Sky) {
            _playerMove.speed = 5f;
            _playerMove.jumpPower = 5f;
        }
        else if (GameManager.Instance.PlayerState == AreaState.Sea) {
            _playerMove.speed = 3f;
            _playerMove.jumpPower = 8f;
        }
    }
    public void SetStateChanged()
    {
        if(GameManager.Instance.PlayerState == AreaState.Sea)
        {
            GameManager.Instance.PlayerState = AreaState.Sky;
        }
        else if(GameManager.Instance.PlayerState == AreaState.Sky)
        {
            GameManager.Instance.PlayerState = AreaState.Sea;
        }
        ChangedState();
    }
}
