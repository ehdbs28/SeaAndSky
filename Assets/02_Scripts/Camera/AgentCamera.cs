using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentCamera : MonoBehaviour
{
    public Camera _camera;
    public CameraMove _cameraMove = null;
    public float dampTime = 0.15f;
    protected float cameraMovementX = 0.5f;
    protected float cameraMovementY = 0.5f;
    protected Vector3 velocity = Vector3.zero;
    public AreaState _cameraState;
    protected Vector3 point;
    //public AgentCameraMove _cameraMove = null;
    private void Awake()
    {
        _cameraMove = FindObjectOfType<CameraMove>();
        _camera = GetComponent<Camera>();
    }
    public virtual void CameraMoving(Transform target)
    {

    }
    
}
