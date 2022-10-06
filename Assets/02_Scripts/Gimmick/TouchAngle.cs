using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAngle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Slash")
        {
            //transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
    }

    public void ChangeAngle()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }
}
