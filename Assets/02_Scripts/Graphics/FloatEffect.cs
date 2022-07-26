using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatEffect : MonoBehaviour
{
    [SerializeField] private float distance = 0.7f;
    [SerializeField] private float delay = 1.5f;
    private Sequence seq;

    void Start()
    {
        seq = DOTween.Sequence();
        Vector3 originalPos = transform.position;

        seq.Append(transform.DOMove(originalPos + Vector3.up * distance, delay).SetEase(Ease.InOutQuad));
        seq.Append(transform.DOMove(originalPos, delay).SetEase(Ease.InOutQuad));
        seq.SetLoops(-1);
    }

    private void OnDestroy()
    {
        seq.Kill();
    }
}
