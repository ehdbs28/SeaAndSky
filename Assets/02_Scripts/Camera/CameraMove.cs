using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CameraMove : MonoBehaviour
{
    public Transform target;
    public UnityEvent<Transform> MoveCamera;
    public UnityEvent ChangeCameaState;
    private void Awake()
    {
        if (target == null)
        {
            target = FindObjectOfType<PlayerMove>().transform;
        }
    }
    
    private void LateUpdate()
    {
        MoveToTarget();
    }
    public void MoveToTarget()
    {
        MoveCamera?.Invoke(target);      
    }
}
