using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDecision : AIDecision
{
    public float _distance = 3f;

    public override bool MakeDecision()
    {
        float distance = Vector2.Distance(_monster.target.position, _monster.transform.position);
        if(distance <= _distance)
        {
            return true;
        }
        return false;
    }
}
