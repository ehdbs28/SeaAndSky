using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullBox : MonoBehaviour
{
    [SerializeField] private float distance = 3f;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        float cal = Vector2.Distance(player.transform.position, transform.position);
        if(cal < distance)
        {
            if (Input.GetKey(KeySetting.keys[Key.pullBox]))
            { 
                player.GetComponent<Player>().Speed = 0.4f;
                transform.position = new Vector2(player.transform.position.x + 1f, transform.position.y); 
            }
            if (Input.GetKeyUp(KeySetting.keys[Key.pullBox]))
            {
                player.GetComponent<Player>().Speed = 5;

            }
        }
    }
}
