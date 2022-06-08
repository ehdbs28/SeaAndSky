using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    public override void TakeAction()
    { 
        Chase();
    }
    private void Chase()
    {
        if (_monster.target == null || !_monster.CheckFrontGround()) return;
        Vector2 dirX = _monster.target.transform.position - _monster.transform.position;

        _monster.transform.Translate(dirX * Time.deltaTime);
    }
}
