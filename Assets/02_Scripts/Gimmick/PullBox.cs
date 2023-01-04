using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullBox : MonoBehaviour
{
    Player player;

    private Vector3 _initPos;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        _initPos = transform.position;
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right,1f, LayerMask.GetMask("Player"));
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, 1f, LayerMask.GetMask("Player"));

        RaycastHit2D hitWall = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, 1f, LayerMask.GetMask("Platform"));
        RaycastHit2D hitWall1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, 1f, LayerMask.GetMask("GrassPlatform"));

        if (!hitWall && !hitWall1)
        {
            if(hit || hit2)
            {
                if (Input.GetKey(KeySetting.keys[Key.pullBox]))
                {
                    player.Speed = 4f;
                    transform.position = new Vector2(player.transform.position.x + (player.VisualObj.localScale.x), transform.position.y);
                }   
                if (Input.GetKeyUp(KeySetting.keys[Key.pullBox]))
                {
                    player.Speed = 6.2f;
                }
            }
        }

        if (hitWall || hitWall1)
        {
            transform.position = new Vector2(transform.position.x , transform.position.y);
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
