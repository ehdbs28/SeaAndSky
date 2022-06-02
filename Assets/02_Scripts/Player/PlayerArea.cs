using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArea : MonoBehaviour
{
    private PlayerMove _playerMove;
    private Rigidbody2D _rigid;

    private void Start()
    {
        _playerMove = GetComponent<PlayerMove>();
        _rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && _playerMove.IsGround)
        {
            SetStateChanged();
        }
        //CheckState();
    }

    public void ChangedState()
    {
        if (GameManager.Instance.PlayerState == AreaState.Sky) {
            _playerMove.LocalScaleY = 1;
            _playerMove.Speed = 5f;
            //_playerMove.JumpPower = 11f;
            _rigid.gravityScale = 3.5f;
        }
        else if (GameManager.Instance.PlayerState == AreaState.Sea) {
            _playerMove.LocalScaleY = -1;
            _playerMove.Speed = 3f;
            //_playerMove.JumpPower = 15f;
            _rigid.gravityScale = -2f;
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

        Vector2 chagedPos = new Vector2(transform.position.x, -transform.position.y);
        //RaycastHit2D hitFloor = Physics2D.Raycast(chagedPos, Vector2.down, 10f, LayerMask.NameToLayer("Plaform"));
        //if(hitFloor)
        //{
        //    transform.position = new Vector2(transform.position.x, hitFloor.point.y + 0.3f);
        //}
        //else
        //{
        //    transform.position = chagedPos;
        //}
        transform.position = chagedPos;
        ChangedState();
    }
}
