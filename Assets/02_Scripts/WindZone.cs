using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    private ParticleSystem windParticle;
    private PlayerMove player;
    private Rigidbody2D rigid;

    [SerializeField]
    private float speed = 1f;
    private const string PLAYER_TAG = "Player";

    void Start()
    {
        windParticle = GetComponentInChildren<ParticleSystem>();
        windParticle.transform.position += Vector3.right * transform.localScale.x * 0.5f;
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = FindObjectOfType<PlayerMove>();
        rigid = player.GetComponentInChildren<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_TAG))
        {
            rigid.position -= Vector2.right * Time.deltaTime * speed;
        }
    }
}
