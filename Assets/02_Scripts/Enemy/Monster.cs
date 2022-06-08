using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : MonoBehaviour
{
    public float _hp;
    public float _deadTime;
    public float _speed;

    public Transform target;
    private bool isDead = false;
    [SerializeField] private AIState _currentState;

    [field: SerializeField] public UnityEvent OnDie { get; set; }
    [field: SerializeField] public UnityEvent OnGetHit { get; set; }

    public void ChangeState(AIState state) 
    {
        _currentState = state;
    }
    private void Update()
    {
        if(target == null)
        {
            return;
        }
        else
        {
            _currentState.UpdateState();
        }
    }
}
