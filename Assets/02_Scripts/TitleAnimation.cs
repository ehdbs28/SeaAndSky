using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleAnimation : MonoBehaviour
{
    [SerializeField] Camera skyCam;
    [SerializeField] Camera seaCam;

    [SerializeField] private float duration;

    [SerializeField] private Ease camEase;

    [Header("Score")]
    [SerializeField] private CanvasGroup scoreText;
    [SerializeField] private List<Text> dieScoreTexts;
    [SerializeField] private List<Text> fishCollectScoreTexts;   

    [Header("Thanks")]
    [SerializeField] private CanvasGroup thanksText;
    [SerializeField] private List<SpriteRenderer> thanksPointLights;
    
    [Header("Title")]
    [SerializeField] private CanvasGroup seaText;
    [SerializeField] private CanvasGroup andText;
    [SerializeField] private CanvasGroup skyText;

    [SerializeField] private CanvasGroup themeGroup;
    [SerializeField] private CanvasGroup stageGroup;

    [SerializeField] private float titleDelay;

    [SerializeField] private Text skipText;

    [Header("Sound")]
    [SerializeField] private AudioClip seaClip;
    [SerializeField] private AudioClip skyClip;
    private AudioSource audioSource;

    private bool isRising = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        Sequence seq = DOTween.Sequence();

        isRising = true;
        seq.AppendInterval(2f);
        seq.Append(seaCam.transform.DOMove(new Vector3(0f, -35f, -10f), duration).SetEase(camEase));
        seq.Join(skyCam.transform.DOMove(new Vector3(0f, 35f, -10f), duration).SetEase(camEase));
        seq.AppendCallback(TitleArrange);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        SkipAnimation();
    }

    private void TitleArrange()
    {
        Time.timeScale = 1f;
        isRising = false;
        skipText.DOFade(0f, 1f);

        Sequence seq = DOTween.Sequence();

        if(DataManager.Instance.User.isClearGame){
            seq.Append(thanksText.DOFade(1f, titleDelay));
            thanksPointLights.ForEach((renderer) => seq.Join(renderer.DOFade(0.4f, titleDelay)));

            seq.Join(scoreText.DOFade(1f, titleDelay));
            dieScoreTexts.ForEach((text) => text.text = $"-  {DataManager.Instance.User.playerDie}");
            fishCollectScoreTexts.ForEach((text) => text.text = $"+  {DataManager.Instance.User.playerFishScore}");
        }

        seq.AppendCallback(() => audioSource.PlayOneShot(seaClip));
        seq.Join(seaText.DOFade(1f, titleDelay));
        seq.Join(seaText.GetComponent<RectTransform>().DOAnchorPosY(0f, titleDelay));

        seq.Append(andText.DOFade(1f, titleDelay));

        seq.AppendCallback(() => audioSource.PlayOneShot(skyClip));
        seq.Join(skyText.DOFade(1f, titleDelay));
        seq.Join(skyText.GetComponent<RectTransform>().DOAnchorPosY(0f, titleDelay));

        seq.AppendCallback(() => {
            foreach(ThemeButton btn in themeGroup.transform.Find("Theme").GetComponentsInChildren<ThemeButton>()){
                btn.Init();
            }
        });
        seq.Join(themeGroup.DOFade(1f, 1f));
    }

    private void SkipAnimation()
    {
        if (isRising && Input.anyKeyDown)
        {
            skipText.DOFade(0f, 1f);
            Time.timeScale = 5.5f;
        }
    }
}