using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Updraft : MonoBehaviour
{
    [Header("Variable")]
    public float distance;
    public float force = 250f;

    public LayerMask targetLayer;

    private Rigidbody2D playerRigid;
    private ParticleSystem[] particles;

    private void Start()
    {
        SetDistance();

        particles = GetComponentsInChildren<ParticleSystem>();
        // 파티클 길이 하기

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((targetLayer & (1 << collision.gameObject.layer)) != 0)
        {
            Rigidbody2D rigid = GetRigidbody(collision);

            rigid.velocity = Vector3.zero;
            rigid?.AddRelativeForce(transform.up * force);
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

    private void SetDistance()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();

        Vector2 size = collider.size;
        Vector2 offset = collider.offset;

        size.y = distance;
        offset.y = distance / 2;

        collider.size = size;
        collider.offset = offset;
    }
}
