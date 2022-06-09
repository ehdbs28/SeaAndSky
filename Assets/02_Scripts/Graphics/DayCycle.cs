using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DayCycle : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float waitTime;

    [SerializeField]
    private Color[] colors;

    private Camera cam;

    private int index = 0;
    private int nextIndex = 1;

    private void Start()
    {
        cam = GetComponent<Camera>();
        StartCoroutine(ChangeSkyColor());
    }

    private IEnumerator ChangeSkyColor()
    {
        yield return new WaitForSeconds(waitTime);

        float timer = 0f;
        float lerpTime = 0f;

        while (true)
        {
            lerpTime += Time.deltaTime * speed;

            cam.backgroundColor = Color.Lerp(colors[index], colors[nextIndex], lerpTime);

            if(cam.backgroundColor.Equals(colors[nextIndex]))
            {
                timer += Time.deltaTime;
            }

            if (timer > waitTime)
            {
                timer = 0f;
                lerpTime = 0f;
                index = (index + 1) % colors.Length;
                nextIndex = (index + 1) % colors.Length;
            }

            yield return null;
        }
    }
}
