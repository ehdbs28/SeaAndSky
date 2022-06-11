using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    private Monster _monster;
    public List<AIAction> _actions = null;
    public List<AITransition> _transitions = null;
    
    private void Awake()
    {
        _monster = GetComponent<Monster>();
    }

    public void UpdateState()
    {
        foreach(AIAction action in _actions)
        {
            action.TakeAction();
        }
        foreach(AITransition transition in _transitions)
        {
            bool result = false;
            foreach(AIDecision decision in transition.decisions)
            {
                result = decision.MakeDecision();
                if (!result) break;
            }
            if(result)
            {
                if(transition.positiveState != null)
                {
                    _monster.ChangeState(transition.positiveState);
                    return;
                }
                else if(transition.negativeState != null)
                {
                    _monster.ChangeState(transition.negativeState);
                    return;
                }
            }
        }
    }
}
