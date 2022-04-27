using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CameraMove : MonoBehaviour
{
    public Transform target;
    public UnityEvent<Transform> MoveCamra;
    public UnityEvent<Transform> ChangeCameaState;
    private void Awake()
    {
        if (target == null)
            target = transform.Find("Player");
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           ChangeCameaState?.Invoke(target);
        }
        MoveToTarget();
    }
    public void MoveToTarget()
    {
        MoveCamra?.Invoke(target);      
    }
}
