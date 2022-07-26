using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject _title;
    [SerializeField] private Image _gameStart;
    [SerializeField] RectTransform _stagePanelTrm;

    [Header("Stage")]
    [SerializeField] StageSO stages;
    [SerializeField] StageButton _stageButton;

    private Vector3 _initPos;

    private bool _isStage = false;

    private const float STAGE_PANEL_Y = -1200f;

    private void Start()
    {
        _initPos = new Vector3(0, STAGE_PANEL_Y);
        _stagePanelTrm.anchoredPosition = _initPos;
        StartCoroutine(FadeCoroutine());
        GenerateStageButtons();
    }

    private void Update()
    {
        if (_isStage)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                InactiveStagePanel();
            }
        }
    }

    IEnumerator FadeCoroutine()
    {
        while (true)
        {
            _gameStart?.CrossFadeAlpha(0, 1f, true);
            yield return new WaitForSeconds(0.5f);
            _gameStart?.CrossFadeAlpha(1, 1f, true);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void StagePanel()
    {
        _stagePanelTrm.DOAnchorPosY(0, 0.5f);
        _isStage = true;
    }

    public void InactiveStagePanel()
    {
        _stagePanelTrm.DOAnchorPosY(STAGE_PANEL_Y, 0.5f);
        _isStage = false;
    }

    private void GenerateStageButtons()
    {
        for (int i = 0; i < stages.stages.Count; ++i)
        {
            StageButton button = Instantiate(_stageButton, _stageButton.transform.parent);
            button.Init(i + 1);
        }

        _stageButton.gameObject.SetActive(false);
    }
}