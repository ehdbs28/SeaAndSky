using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IdleAction : AIAction
{
    private AIAnimationData _aIAnimationData;
    protected override void ChildAwake()
    {
        _aIAnimationData = transform.parent.GetComponent<AIAnimationData>();
    }
    public override void TakeAction()
    {
        if(_aIAnimationData._Animator.GetBool("Walk"))
        {
            _aIAnimationData._Animator.SetBool("Walk", false);
        }
    }
}
