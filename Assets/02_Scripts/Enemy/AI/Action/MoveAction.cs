using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MoveAction : AIAction
{

    public override void TakeAction()
    {
        Move();
    }

    private void Move()
    {
        if(_monster.CheckFrontGround() && _monster.CheckFrontWall() == false)
        {
            _monster.transform.Translate(_monster.MonsterDir * Time.deltaTime);
        }
        else
        {
            _monster.MonsterDir *= -1;
        }
    }
   
}
