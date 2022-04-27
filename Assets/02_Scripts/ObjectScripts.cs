using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScripts : MonoBehaviour
{
    [SerializeField]
    private Transform targetTransform;

    private Color color;

    void Start()
    {
        ObjectSetting();
    }


    void ObjectSetting()
    {
        transform.position 
            = new Vector3(targetTransform.position.x, -targetTransform.position.y);

        color.a = .5f; color.r = 1f; color.g = 1f; color.b = 1f;

        GetComponent<SpriteRenderer>().color = color;
    }
}
