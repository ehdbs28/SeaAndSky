using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{
    protected Monster _monster;

    private void Awake()
    {
        _monster = transform.GetComponentInParent<Monster>();

        ChildAwake();
    }

    private void ChildAwake()
    {
       
    }
    public abstract bool MakeDecision();
}
