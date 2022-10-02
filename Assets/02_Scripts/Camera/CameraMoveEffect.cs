using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMoveEffect : MonoBehaviour
{
    public float xSpeed = 35f;
    public float ySpeed = 25f;

    public float distanceX = 0f;
    public float distanceY = 0.08f;

    private float angle = 0f;

    private Camera cam;
    private float maxZoomOut;
    private float orthographicSize;

    private void Start()
    {
        cam = GetComponent<Camera>();
        orthographicSize = cam.orthographicSize;
        maxZoomOut = orthographicSize + 0.3f;
    }

    private void Update()
    {
        angle += Time.deltaTime;
    }

    public void MoveCamera()
    {
        Vector3 dest = transform.position;

        dest.x += distanceX * Mathf.Sin(angle * xSpeed * Mathf.Deg2Rad) * Time.deltaTime;
        dest.y += distanceY * Mathf.Sin(angle * ySpeed * Mathf.Deg2Rad) * Time.deltaTime;

        transform.position = dest;
    }

    public void ZoomIn()
    {
        cam?.DOKill();
        cam?.DOOrthoSize(orthographicSize, 1f);
    }

    public void ZoomOut()
    {
        cam?.DOKill();
        cam?.DOOrthoSize(maxZoomOut, 20f);
    }

    public void ResetData()
    {
        angle = 0f;
    }
}