using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDie : MonoBehaviour
{
    public bool isDeath = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isDeath = true;
        }
    }
}
