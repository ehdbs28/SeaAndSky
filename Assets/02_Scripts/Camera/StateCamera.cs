using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCamera : AgentCamera
{
    public Transform target;
    CameraMoveEffect camEffect;

    private bool isChecking = false;
    private bool isZoomCheck = false;

    public bool IsMainCam => GameManager.Instance.PlayerState == _cameraState;

    private Vector3 playerDirection = Vector3.zero;

    private void Start()
    {
        Player player = FindObjectOfType<Player>();
        player.OnPlayerMove.AddListener(CameraMoving);
        target = player.transform;

        camEffect = GetComponent<CameraMoveEffect>();

        transform.position = GetDestination(target.position);
    }

    private void LateUpdate()
    {
        if (playerDirection.magnitude < 0.01f)
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

    public override void CameraMoving(Transform target)
    {
        if(IsDownView){
            MoveDownView();
        }
        else{
            if (GameManager.Instance.PlayerState == _cameraState)
            {
                transform.position = Vector3.SmoothDamp(transform.position, GetDestination(target.position), ref velocity, dampTime);
            }
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position, GetDestination(target.position), ref velocity, dampTime);
            }
        }
    }

    public void MoveDownView(){
        if(IsDownView){
            transform.position = Vector3.SmoothDamp(transform.position, GetDestination(new Vector3(MoveDownValue.x, MoveDownValue.y * Mathf.Sign(GameManager.Instance.CurrentCam.transform.position.y), MoveDownValue.z)), ref velocity, dampTime);
        }
    }

    private Vector3 GetDestination(Vector3 target)
    {
        Vector3 destination;

        if (GameManager.Instance.PlayerState == _cameraState)
        {
            point = _camera.WorldToViewportPoint(target);
            Vector3 delta = (target - _camera.ViewportToWorldPoint(new Vector3(cameraMovementX, cameraMovementY, point.z)));
            destination = transform.position + delta;

        }
        else
        {
            point = _camera.WorldToViewportPoint(new Vector3(target.x, -target.y, target.z));
            Vector3 delta = (new Vector3(target.x, -target.y, target.z) - _camera.ViewportToWorldPoint(new Vector3(cameraMovementX, cameraMovementY, point.z)));
            destination = transform.position + delta;
        }

        return destination;
    }

    private void CameraMoving(Vector2 dir)
    {
        playerDirection = dir;
    }

    private IEnumerator ZoomDelay()
    {
        isZoomCheck = true;
        yield return new WaitForSeconds(3f);
        camEffect.ZoomOut();
        isZoomCheck = false;
    }
}