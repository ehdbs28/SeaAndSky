using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectItem : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnObject;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            SpawnObject();
        }
    }


    void SpawnObject()
    {
        spawnObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
