using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    private ParticleSystem[] windParticles;
    private Player player;
    private Rigidbody2D rigid;

    [SerializeField] private float windForce = 250f;

    [SerializeField]
    private float speed = 1f;
    private const string PLAYER_TAG = "Player";

    void Start()
    {
        windParticles = GetComponentsInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = FindObjectOfType<Player>();

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
                //rigid.position += Vector2.left * Time.deltaTime * windForce;
                rigid.AddForce(Vector2.left * windForce);
                //rigid.AddRelativeForce(Vector2.left * windForce, ForceMode2D.Force);
            }
        }
    }
}
