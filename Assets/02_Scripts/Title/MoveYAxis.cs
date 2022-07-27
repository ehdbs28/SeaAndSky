using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveYAxis : MonoBehaviour
{
    [SerializeField] private bool isUp;
    [SerializeField] private float speed = 1f;

    private void Update()
    {
        if (Mathf.Abs(transform.position.x) > 100f)
        {
            gameObject.SetActive(false);
        }

        if (isUp)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        else
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
    }
}
