using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDecision : AIDecision
{
    public float distance = 3f;

    public override bool MakeDecision()
    {
        float distanceX = Mathf.Abs(_monster.target.position.x - _monster.transform.position.x); 
        if(distanceX <= 3f)
        {
            return true;
        }
        return false;
    }
}
