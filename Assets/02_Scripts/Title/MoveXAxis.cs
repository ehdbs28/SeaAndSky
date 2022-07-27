using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveXAxis : MonoBehaviour
{
    [SerializeField] private bool isLeft;
    [SerializeField] private float speed = 1f;

    private void Update()
    {
        if(Mathf.Abs(transform.position.x) > 100f)
        {
            gameObject.SetActive(false);
        }

        if(isLeft)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
    }
}
