using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    private ParticleSystem[] windParticles;
    private PlayerMove player;
    private Rigidbody2D rigid;

    [SerializeField] private float windForce = 250f;

    [SerializeField]
    private float speed = 1f;
    private const string PLAYER_TAG = "Player";

    // 24 1.3
    void Start()
    {
        windParticles = GetComponentsInChildren<ParticleSystem>();
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
                //rigid.position -= Vector2.right * Time.deltaTime * speed;
                rigid.AddForce(Vector2.left * windForce);
            }
        }
    }
}
