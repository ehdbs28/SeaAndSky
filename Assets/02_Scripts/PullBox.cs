using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullBox : MonoBehaviour
{
    //[SerializeField] private float distance = 3f;
    //[SerializeField] private GameObject player;
    PlayerMove player;

    private Vector3 _initPos;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
        _initPos = transform.position;
    }

    RaycastHit2D hit;

    private void Update()
    {
        float cal = Vector2.Distance(player.transform.position, transform.position);

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right,1f, LayerMask.GetMask("Player"));
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, 1f, LayerMask.GetMask("Player"));

        if(hit || hit2)
        {
            if (Input.GetKey(KeySetting.keys[Key.pullBox]))
            {
                //이동방향 * 플레이어 스피드
                player.Speed = 0.4f;
                transform.position = new Vector2(player.transform.position.x + ((player.isLeft) ? -1f : 1f), transform.position.y);

                //Debug.DrawRay(this.transform.position , Vector3.left * 3f, Color.red, LayerMask.GetMask("Player"));
            }   
            if (Input.GetKeyUp(KeySetting.keys[Key.pullBox]))
            {
                player.Speed = 5;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            transform.position = _initPos;
        }
    }
}
