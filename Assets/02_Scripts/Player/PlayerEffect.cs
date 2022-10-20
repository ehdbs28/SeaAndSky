using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerEffect : MonoBehaviour
{
    [SerializeField] private TrailRenderer seaEffect;
    [SerializeField] private ParticleSystem seaMoveEffect;

    private Material dissolveMaterial;
    [SerializeField] private float dissolveWidth;
    [SerializeField] private float dissolveHeight;

    private readonly int fadeID = Shader.PropertyToID("_Fade");
    private readonly int colorID = Shader.PropertyToID("_Color");

    private void Awake()
    {
        dissolveMaterial = transform.Find("VisualSprite").GetComponent<Renderer>().material;
        dissolveMaterial.SetFloat("_Width", 1 / dissolveWidth);
        dissolveMaterial.SetFloat("_Height", 1 / dissolveHeight);
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
        seaEffect.Clear();

        if (areaState == AreaState.Sea)
        {
            dissolveMaterial.SetColor(colorID, Color.cyan);
            seaEffect.gameObject.SetActive(true);
        }
        else
        {
            dissolveMaterial.SetColor(colorID, Color.yellow);
            seaEffect.gameObject.SetActive(false);
        }

        dissolveMaterial.DOKill();
        dissolveMaterial.SetFloat(fadeID, 0f);
        dissolveMaterial.DOFloat(1f, fadeID, 1.5f);
    }

    public void OnReverseEffect(bool isBegin)
    {
        if(isBegin)
        {
            dissolveMaterial.DOFloat(0.5f, "_Intensity", 1f);
        }
        else
        {
            dissolveMaterial.DOFloat(0f, "_Intensity", 0.3f);
        }
    }
}
