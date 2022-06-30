using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{
    protected Monster _monster;

    private void Awake()
    {
        _monster = GetComponentInParent<Monster>();
        ChildAwake();
    }
    protected virtual void ChildAwake()
    {

    }
    public abstract void TakeAction();
}
