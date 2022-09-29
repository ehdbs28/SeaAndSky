using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Updraft : MonoBehaviour
{
    public float force = 250f;

    public LayerMask targetLayer;

    private Rigidbody2D playerRigid;
    private ParticleSystem[] particles;

    private void Start()
    {
        GetComponent<SpriteRenderer>().color = Color.clear;

        // 파티클 길이 하기
        particles = transform.parent.GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem ps in particles)
        {
            var main = ps.main;
            main.startLifetime = transform.localScale.y;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((targetLayer & (1 << collision.gameObject.layer)) != 0)
        {
            Rigidbody2D rigid = GetRigidbody(collision);

            if (rigid)
            {
                rigid.velocity = Vector3.zero;
                rigid?.AddRelativeForce(transform.up * force);
            }
        }
    }

    Rigidbody2D GetRigidbody(Collider2D collision)
    {
        Rigidbody2D rigid;

        if (collision.CompareTag("Player"))
        {
            if (!playerRigid)
            {
                playerRigid = collision.GetComponent<Rigidbody2D>();
            }

            rigid = playerRigid;
        }
        else
        {
            rigid = collision.GetComponent<Rigidbody2D>();
        }

        return rigid;
    }
}
