using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackJudgement : MonoBehaviour
{
    
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //피격시 튕기게
        if (collision.CompareTag("Enemy"))
        {
            TimeController.Instance.ModifyTimeScale(0.5f, 0.1f, () =>
            {
                TimeController.Instance.ModifyTimeScale(1f, 0.15f);
            });

            GameObject playerObject = GameObject.Find("Player");
            Rigidbody2D py = playerObject.GetComponent<Rigidbody2D>();

            if (Input.GetKey(KeyCode.DownArrow))
                py.velocity = Vector3.up * jumpPower;

            else if(!PlayerMove.isLeft)
                py.velocity = Vector3.left * speed;
            
            else if(PlayerMove.isLeft)
                py.velocity = Vector3.right * speed;
        }
    }
}
