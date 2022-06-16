using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    public List<AIDecision> decisions;
    
    public AIState positiveState;
    public AIState negativeState;
}
