using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackJudgement : MonoBehaviour
{
    
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    private void Start()
    {

    }

    private void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //피격시 튕기게
        if (collision.CompareTag("Enemy"))
        {

         
            PlayerMove playerObject = FindObjectOfType<PlayerMove>();
            Rigidbody2D py = playerObject.GetComponent<Rigidbody2D>();
            if (Input.GetKey(KeyCode.DownArrow))
                py.velocity = Vector3.up * jumpPower;

            else if (!playerObject.isLeft)
                py.velocity = Vector3.left * speed;

            else if (playerObject.isLeft)
                py.velocity = Vector3.right * speed;
            IHittable hittable = collision.GetComponent<IHittable>();
            hittable?.GetHit();
        }
    }
}
