using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePlatSpawn : MonoBehaviour
{
    private Vector3 dir1;

    private void Awake()
    {
        dir1 = this.transform.position;
    }

    private void Start()
    {
        
    }
}
