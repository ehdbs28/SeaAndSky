using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePlat : MonoBehaviour
{
    [SerializeField] private float fallTime = 0.7f;
    [SerializeField] private float destroyTime = 3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

        }
    }
}
