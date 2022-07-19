using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackJudgement : MonoBehaviour
{
    [SerializeField] private float jumpPower;
    private bool isAttack =false;
    //public UnityEvent AttackFeedback;

    private void OnTriggerEnter2D(Collider2D collision)
    { 
    
        if (isAttack) return;
        isAttack = true;
        //�ǰݽ� ƨ���?
        PlayerMove playerObject = GameObject.Find("Player").GetComponent<PlayerMove>();
        Rigidbody2D py = playerObject.GetComponent<Rigidbody2D>();
        IHittable hittable = collision.GetComponent<IHittable>();
        hittable?.GetHit();
        if (Input.GetKey(KeyCode.DownArrow) && hittable != null && GameManager.Instance.PlayerState == AreaState.Sky && !playerObject.IsGround)
        {
            py.velocity = Vector2.zero;
            py.velocity = Vector3.up * jumpPower;
        }
        else if (collision.CompareTag("Trap") || collision.CompareTag("Enemy")) 
        {
            py.velocity = Vector2.zero;
            py.velocity = Vector3.up * jumpPower;
        } 
    }
}