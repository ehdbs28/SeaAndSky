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
            ChagedState();
        }
    }
    public void CheckState()
    {
        if (GameManager.Instance.PlayerState == AreaState.Sky) {
            StateToSky(); 
        }
        else if (GameManager.Instance.PlayerState == AreaState.Sea) { 
            StateToSea(); 
        }
    }
    public void ChagedState()
    {
        if(GameManager.Instance.PlayerState == AreaState.Sea)
        {
            GameManager.Instance.PlayerState = AreaState.Sky;
        }
        else if(GameManager.Instance.PlayerState == AreaState.Sky)
        {
            GameManager.Instance.PlayerState = AreaState.Sea;
        }
        CheckState();
    }
    private void StateToSea()
    {
        Debug.Log("Sea");
        _playerMove.speed = 3f;
        _playerMove.jumpPower = 8f;
    }
    private void StateToSky()
    {
        Debug.Log("Sky");
        _playerMove.speed = 5f;
        _playerMove.jumpPower = 5f;
    }
}
