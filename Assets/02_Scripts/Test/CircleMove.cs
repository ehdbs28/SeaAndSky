using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector3 lastVelocity;
    Vector3 initDir;
    public float spawnTime = 0.3f;

    private void Start()
    {
        initDir = transform.position;
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        lastVelocity = rigid.velocity;
    }

    public void GoCircle()
    {
        rigid.velocity = new Vector3(1, 0, 0) * 10f;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.collider.name == "Angle")
        {
            var speed = lastVelocity.magnitude;
            var dir = Vector2.Reflect(lastVelocity.normalized, coll.contacts[0].normal);
            rigid.velocity = dir * Mathf.Max(speed, 0f);
        }
        else if(coll.collider.name == "CircleGoal")
        {
            Debug.Log("^0^");
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(ReSpawnCircle());
        }
    }

    IEnumerator ReSpawnCircle()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(spawnTime);
        Debug.Log(12);
        transform.position = initDir;
        gameObject.SetActive(true);
    }
}