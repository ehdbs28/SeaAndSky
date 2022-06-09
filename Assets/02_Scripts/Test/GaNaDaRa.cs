using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GaNaDaRa : MonoBehaviour
{
    public UnityEvent<int> OnButtonPress;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnButtonPress.Invoke(3);
        }
    }
    public void PrintHello(int asd)
    {
        Debug.Log("Hello");
        Debug.Log(asd);
    }
}
