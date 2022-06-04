using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    [SerializeField] private GameObject seaEffect;
    [SerializeField] private ParticleSystem seaMoveEffect;

    private void Update()
    {
        ActiveSeaEffect();
    }

    private void ActiveSeaEffect()
    {
        seaEffect.SetActive(GameManager.Instance.PlayerState == AreaState.Sea);
    }

    public void ActiveSeaMoveEffect(Vector2 direction)
    {
        if(GameManager.Instance.PlayerState == AreaState.Sea && direction.sqrMagnitude > 0.01f)
        {
            if (seaMoveEffect.isPlaying) return;
            seaMoveEffect.Play();
        }
        else
        {
            seaMoveEffect.Stop();
        }
    }
}
