using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ButtonManager : MonoBehaviour //¼öÁ¤
{
    [SerializeField] private TextMeshProUGUI _gameStartTxt;

    private void Start()
    {
        StartCoroutine(FadeCoroutine());
    }

    IEnumerator FadeCoroutine()
    {
        while (true)
        {
            Sequence sq = DOTween.Sequence();

            sq.Append(_gameStartTxt.material.DOFade(0f, 0.5f));

            sq.Append(_gameStartTxt.material.DOFade(1f, 0.5f));

            yield return new WaitForSeconds(0.2f);
        }
    }
}
