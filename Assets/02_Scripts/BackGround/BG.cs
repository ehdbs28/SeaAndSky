using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
    [SerializeField] private Transform _backGroundTrm;
    [SerializeField] private float _bgSpeed = 5f;

    private void Update()
    {
        _backGroundTrm.position += Vector3.left * _bgSpeed * Time.deltaTime;

        if(_backGroundTrm.position.x <= -30)
        {
            _backGroundTrm.position = new Vector3(50, 0, 0);
        }
    }
}
