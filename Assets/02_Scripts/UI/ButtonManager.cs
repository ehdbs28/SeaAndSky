using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private  Image _gameStart;
    [SerializeField] RectTransform _stagePanelTrm;

    private void Start()
    {
        StartCoroutine(FadeCoroutine());
    }

    IEnumerator FadeCoroutine()
    {
        while (true)
        {
            _gameStart.CrossFadeAlpha(0, 1f, true);
            yield return new WaitForSeconds(0.5f);
            _gameStart.CrossFadeAlpha(1, 1f, true);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void StagePanel()
    {
        Sequence sq = DOTween.Sequence();

        
    }

    public void SceneChange()
    {
        //SceneManager.LoadScene(0);
    }
}
