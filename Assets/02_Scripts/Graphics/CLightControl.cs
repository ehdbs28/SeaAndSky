using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CLightControl : MonoBehaviour
{
    public Light2D _light;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            StartCoroutine(SHooot()); 
        }
    }

    public void Shooong()
    {
        _light.pointLightInnerAngle -= Time.deltaTime * 5;
        _light.pointLightOuterAngle -= Time.deltaTime * 5;

        _light.pointLightOuterRadius -= Time.deltaTime * 5;
    }

    IEnumerator SHooot()
    {
       _light.pointLightInnerAngle -= Time.deltaTime * 5;
       _light.pointLightOuterAngle -= Time.deltaTime * 5;
       _light.pointLightOuterRadius -= Time.deltaTime * 5;

        yield return new WaitForSeconds(3);
    }
}
