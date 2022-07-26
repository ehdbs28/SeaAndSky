using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeySetting.keys[Key.jump]))
        {
            Debug.Log("jump");
        }
        if (Input.GetKey(KeySetting.keys[Key.attack]))
        {
            Debug.Log("attack");
        }
        if (Input.GetKey(KeySetting.keys[Key.right]))
        {
            Debug.Log("right");
        }
        if (Input.GetKey(KeySetting.keys[Key.left]))
        {
            Debug.Log("left");
        }

    }
}
