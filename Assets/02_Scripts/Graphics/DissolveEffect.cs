using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DissolveEffect : MonoBehaviour
{
    private Material dissolveMaterial;
    private readonly int fadeID = Shader.PropertyToID("_Fade");
    private readonly int colorID = Shader.PropertyToID("_Color");

    private void Start()
    {
        dissolveMaterial = GetComponent<Renderer>().material;
    }

    public void PlayEffect(AreaState areaState)
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