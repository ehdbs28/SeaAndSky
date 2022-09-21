using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject outPotal;
    [SerializeField] private ParticleSystem outEffect;

    private Vector3 outDir;

    private void Start()
    {
        outDir = outPotal.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerCol"))
        {
            TeleportTime();
        }
    }

    public void TeleportTime()
    {
        player.transform.position = outDir;
        outEffect.Play();
    }
}
