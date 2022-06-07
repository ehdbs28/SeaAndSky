using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MoveRandomLight : MonoBehaviour
{
    private float timer = 0f;
    private float randomTime;

    private float randomIntensity = 0f;

    new private Light2D light;
    private Vector3 targetPosition;

    private void Start()
    {
        light = GetComponent<Light2D>();
        SetRandomValue();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > randomTime)
        {
            SetRandomValue();
        }
        else
        {
            light.intensity = Mathf.Lerp(light.intensity, randomIntensity, timer / randomTime);
            transform.position = Vector2.Lerp(transform.position, targetPosition, timer / randomTime);
        }
    }

    private void SetRandomValue()
    {
        randomTime = Random.Range(3f, 5f);
        randomIntensity = Random.Range(0.6f, 1f);
        targetPosition = transform.position;
        targetPosition.x += Random.Range(-0.5f, 0.5f);
        timer = 0f;
    }
}
