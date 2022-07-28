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

    [Header("Title")]
    [SerializeField] private CanvasGroup seaText;
    [SerializeField] private CanvasGroup andText;
    [SerializeField] private CanvasGroup skyText;

    [SerializeField] private CanvasGroup stageGroup;

    [SerializeField] private float titleDelay;

    [Header("Sound")]
    [SerializeField] private AudioClip seaClip;
    [SerializeField] private AudioClip skyClip;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        Sequence seq = DOTween.Sequence();

        seq.AppendInterval(2f);
        seq.Append(seaCam.transform.DOMove(new Vector3(0f, -5f, -10f), duration).SetEase(camEase));
        seq.Join(skyCam.transform.DOMove(new Vector3(0f, 5f, -10f), duration).SetEase(camEase));
        seq.AppendCallback(TitleArrange);
    }

    private void TitleArrange()
    {
        Sequence seq = DOTween.Sequence();

        seq.AppendCallback(() => audioSource.PlayOneShot(seaClip));
        seq.Join(seaText.DOFade(1f, titleDelay));
        seq.Join(seaText.transform.DOMoveY(0f, titleDelay));

        seq.Append(andText.DOFade(1f, titleDelay));

        seq.AppendCallback(() => audioSource.PlayOneShot(skyClip));
        seq.Join(skyText.DOFade(1f, titleDelay));
        seq.Join(skyText.transform.DOMoveY(0f, titleDelay));

        seq.Append(stageGroup.DOFade(1f, 1f));
    }
}