using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerEffect : MonoBehaviour
{
    [SerializeField] private GameObject seaEffect;
    [SerializeField] private ParticleSystem seaMoveEffect;

    private Material dissolveMaterial;

    private readonly int fadeID = Shader.PropertyToID("_Fade");
    private readonly int colorID = Shader.PropertyToID("_Color");

    private void Awake()
    {
        dissolveMaterial = GetComponent<Renderer>().material;
    }

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
        if (GameManager.Instance.PlayerState == AreaState.Sea && direction.sqrMagnitude > 0.01f)
        {
            if (seaMoveEffect.isPlaying) return;
            seaMoveEffect.Play();
        }
        else
        {
            seaMoveEffect.Stop();
        }
    }

    public void PlayDissolveEffect(AreaState areaState)
    {
        if (areaState == AreaState.Sea)
        {
            dissolveMaterial.SetColor(colorID, Color.cyan);
        }
        else
        {
            dissolveMaterial.SetColor(colorID, Color.yellow);
        }

        dissolveMaterial.DOKill();
        dissolveMaterial.SetFloat(fadeID, 0f);
        dissolveMaterial.DOFloat(1f, fadeID, 1.5f);
    }
}
