using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ConstantManager;

public class SpawnObjectItem : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnObject;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        EventManager.StartListening(SPAWN_OBJECT, SpawnObject);
    }

    void SpawnObject()
    {
        spawnObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
