using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CircleMove : MonoBehaviour
{
    public UnityEvent OnPressEvent;
    public float respawnTime = 2f;

    private Vector3 initPos;

    Rigidbody2D rigid;
    Vector3 lastVelocity;
    public float spawnTime = 0.3f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        initPos = transform.position;
    }

    private void Update()
    {
        lastVelocity = rigid.velocity;
    }

    public void GoCircle()
    {
        rigid.velocity = new Vector3(1, 0, 0) * 10f;
    }

    IEnumerator ReSpawnCoroutine(){
        rigid.velocity = Vector2.zero;

        transform.position = initPos;
        gameObject.SetActive(false);
        yield return new WaitForSeconds(respawnTime);
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.name == "Angle")
        {
            var speed = lastVelocity.magnitude;
            var dir = Vector2.Reflect(lastVelocity.normalized, coll.contacts[0].normal);
            rigid.velocity = dir * Mathf.Max(speed, 0f);
        }
        else if (coll.collider.name == "CircleGoal")
        {
            OnPressEvent?.Invoke();
            Destroy(gameObject);
        }

        if(coll.transform.CompareTag("Player") || coll.gameObject.layer == 6){
            StartCoroutine(ReSpawnCoroutine());
        }
    }
}