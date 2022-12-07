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
    public Vector3 MoveDownValue = new Vector3(0f, -3f, 0f);
    public AreaState _cameraState;
    public bool IsDownView = false;
    protected Vector3 point;
    private void Awake()
    {
        _cameraMove = FindObjectOfType<CameraMove>();
        _camera = GetComponent<Camera>();
    }
    public virtual void CameraMoving(Transform target)
    {
        
    }
    
}
