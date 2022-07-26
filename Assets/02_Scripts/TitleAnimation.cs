using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleAnimation : MonoBehaviour
{
    [SerializeField] Camera skyCam;
    [SerializeField] Camera seaCam;

    [SerializeField] private float duration;

    [SerializeField] private Ease camEase;

    private void Start()
    {
        seaCam.transform.DOMove(new Vector3(0f, -5f, -10f), duration).SetEase(camEase);
        skyCam.transform.DOMove(new Vector3(0f, 5f, -10f), duration).SetEase(camEase);
    }
}
