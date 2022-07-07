using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackJudgement : MonoBehaviour
{
    [SerializeField] private float jumpPower;
    //public UnityEvent AttackFeedback;

    private void OnTriggerEnter2D(Collider2D collision)
    {
            //피격시 튕기게
            PlayerMove playerObject = GameObject.Find("Player").GetComponent<PlayerMove>();
            Rigidbody2D py = playerObject.GetComponent<Rigidbody2D>();
            IHittable hittable = collision.GetComponent<IHittable>();

            if (Input.GetKey(KeyCode.DownArrow) && hittable != null && GameManager.Instance.PlayerState == AreaState.Sky && !playerObject.IsGround)
            {
                py.velocity = Vector2.zero;
                py.velocity = Vector3.up * jumpPower;
                hittable?.GetHit();
        }
    }
}