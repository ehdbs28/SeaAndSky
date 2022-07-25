using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    private ParticleSystem[] windParticles;
    private PlayerMove player;
    private Rigidbody2D rigid;

    private BoxCollider2D collider;

    [SerializeField]
    private float speed = 1f;
    private const string PLAYER_TAG = "Player";

    // 24 1.3
    void Start()
    {
        windParticles = GetComponentsInChildren<ParticleSystem>();
        collider = GetComponent<BoxCollider2D>();

        foreach (ParticleSystem particle in windParticles)
        {
            //Vector3 pos = particle.transform.position;
            //pos.x += collider.bounds.max.x;

            //collider.
            //particle.transform.position = pos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("sdf");
            player = FindObjectOfType<PlayerMove>();

            if (player)
            {
                rigid = player.GetComponentInChildren<Rigidbody2D>();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_TAG))
        {
            if (rigid)
            {
                Debug.Log("sdf");
                rigid.position -= Vector2.right * Time.deltaTime * speed;
            }
        }
    }
}
