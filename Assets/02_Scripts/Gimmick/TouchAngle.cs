using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAngle : MonoBehaviour
{
    public void ChangeAngle()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1);
    }
}
