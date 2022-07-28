using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackJudgement : MonoBehaviour
{
    [SerializeField] private float jumpPower;
    //public UnityEvent AttackFeedback;

    private PlayerMove playerObject;
    private Rigidbody2D py;

    private void Start()
    {
        playerObject = GameObject.Find("Player").GetComponent<PlayerMove>();
        py = playerObject.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHittable hittable = collision.GetComponent<IHittable>();
        hittable?.GetHit();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.DownArrow) && !playerObject.IsGround && (collision.CompareTag("Trap") || collision.CompareTag("Enemy")))
        {
            py.velocity = Vector2.zero;
            py.velocity = Vector3.up * jumpPower;
        }
    }
}