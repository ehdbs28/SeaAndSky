using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCamera : AgentCamera
{
    public override void CameraChagned(Transform target)
    {
        if (GameManager.Instance._State == _cameraState)
        {
            GameManager.Instance._State = changeState;
        }
	}
    public override void CameraMoving(Transform target)
    {
        if (GameManager.Instance._State == _cameraState)
        {
		    point = _camera.WorldToViewportPoint(target.position);
            Vector3 delta =(target.position - _camera.ViewportToWorldPoint(new Vector3(cameraMovementX, cameraMovementY, point.z)));
			Vector3 destination = transform.position + delta;
			this.transform.position = Vector3.SmoothDamp(this.transform.position, destination, ref velocity, dampTime);
		}
    }
}
