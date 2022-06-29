using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Events;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject _title;
    [SerializeField] private  Image _gameStart;
    [SerializeField] RectTransform _stagePanelTrm;

    public UnityEvent OnChangeScene;
    private Vector3 _initPos;

    private bool _isStage = false;

    private void Start()
    {
        _initPos = new Vector3(0, -1200);
        _stagePanelTrm.anchoredPosition = _initPos;
        StartCoroutine(FadeCoroutine());
    }

    private void Update()
    {
        if (_isStage)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Sequence sq = DOTween.Sequence();

                _title.gameObject.SetActive(true);
                _gameStart.gameObject.SetActive(true);
                sq.Append(_stagePanelTrm.DOAnchorPosY(-1200, 0.5f));
                _isStage = false;
            }
        }
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

        _title.gameObject.SetActive(false);
        _gameStart.gameObject.SetActive(false);
        sq.Append(_stagePanelTrm.DOAnchorPosY(0, 0.5f));
        _isStage = true;
    }

    public void SceneChange(string name)
    {
        OnChangeScene.Invoke();
        SceneManager.LoadScene(name);
    }
}
