using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    [SerializeField] private GameObject _door;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(x, y) * _speed * Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision) //닿아 있을때 실행되는 함수
    {
        if (collision.CompareTag("Button"))
        {
            _door.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if(collision.CompareTag("Button"))
        {
            _door.SetActive(true);    
        }
    }
}
