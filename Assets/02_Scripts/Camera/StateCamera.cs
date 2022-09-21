using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCamera : AgentCamera
{
    public Transform target;
    CameraMoveEffect camEffect;

    private bool isChecking = false;
    private bool isZoomCheck = false;

    private void Start()
    {
        PlayerMove playerMove = FindObjectOfType<PlayerMove>();
        playerMove.onPlayerMove.AddListener(CameraMoving);
        target = playerMove.transform;

        camEffect = GetComponent<CameraMoveEffect>();

        transform.position = GetDestination();
    }

    private void Update()
    {

    }

    public override void CameraMoving(Transform target)
    {
        if (GameManager.Instance.PlayerState == _cameraState)
        {
            transform.position = Vector3.SmoothDamp(transform.position, GetDestination(), ref velocity, dampTime);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, GetDestination(), ref velocity, dampTime);
        }
    }

    private Vector3 GetDestination()
    {
        Vector3 destination;

        if (GameManager.Instance.PlayerState == _cameraState)
        {
            point = _camera.WorldToViewportPoint(target.position);
            Vector3 delta = (target.position - _camera.ViewportToWorldPoint(new Vector3(cameraMovementX, cameraMovementY, point.z)));
            destination = transform.position + delta;

        }
        else
        {
            point = _camera.WorldToViewportPoint(new Vector3(target.position.x, -target.position.y, target.position.z));
            Vector3 delta = (new Vector3(target.position.x, -target.position.y, target.position.z) - _camera.ViewportToWorldPoint(new Vector3(cameraMovementX, cameraMovementY, point.z)));
            destination = transform.position + delta;
        }

        return destination;
    }

    private void CameraMoving(Vector2 dir)
    {
        if (dir.magnitude < 0.01f)
        {
            if (!isChecking)
            {
                camEffect.ResetData();

                if (!isZoomCheck)
                    StartCoroutine(ZoomDelay());
            }

            isChecking = true;
            camEffect.MoveCamera();
        }
        else
        {
            if (isChecking)
            {
                camEffect.ZoomIn();
            }

            isChecking = false;
        }

        CameraMoving(target);
    }

    private IEnumerator ZoomDelay()
    {
        isZoomCheck = true;
        yield return new WaitForSeconds(3f);
        camEffect.ZoomOut();
        isZoomCheck = false;
    }
}