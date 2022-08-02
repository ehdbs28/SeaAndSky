using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportItem : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    void Start()
    {
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerTeleport();
        }
    }

    void PlayerTeleport()
    {
        gameObject.SetActive(false);
    }
}
