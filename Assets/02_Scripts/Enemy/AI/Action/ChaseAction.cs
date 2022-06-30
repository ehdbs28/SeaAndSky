using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    private AIAnimationData _aIAnimationData;
    protected override void ChildAwake()
    {
        _aIAnimationData = transform.parent.GetComponent<AIAnimationData>();
    }
    public override void TakeAction()
    {
        if (!_aIAnimationData._Animator.GetBool("Walk"))
        {
            _aIAnimationData._Animator.SetBool("Walk", true);
        }
        Vector2 dir = _monster.target.position - _monster.transform.position;
        _monster.transform.Translate(dir * Time.deltaTime);
    }
}
