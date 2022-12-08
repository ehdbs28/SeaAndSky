using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizonGraphicSetting : MonoBehaviour
{
    [SerializeField] private Camera _cam;

    private void Update()
    {
        transform.localPosition = new Vector3(0, _cam.orthographicSize * -Mathf.Sign(transform.localScale.y), 1);
        transform.localScale = new Vector3(1, Mathf.Sign(transform.localScale.y) * 0.3f, 1) * _cam.orthographicSize * 0.2f;
    }
}
