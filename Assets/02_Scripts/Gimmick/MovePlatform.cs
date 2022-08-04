using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MovePlatform : MonoBehaviour
{
    [Header("상호작용 없이 움직일 경우 필요한 변수")]
    [SerializeField]
    private bool _isAuto = false;
    [SerializeField]
    private float _stopDelay;

    [SerializeField]
    private float offsetX, offsetY;
    [SerializeField]
    private float moveTime = 1f;

    [SerializeField]
    private AudioClip moveSound;
    private ParticleSystem moveParticle;

    private Vector2 originVector;
    private Sequence seq;

    private void Start()
    {
        moveParticle = GetComponentInChildren<ParticleSystem>();

        originVector = transform.position;
        if (_isAuto)
        {
            StartCoroutine(AutomaticMoveCoroutine());
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isAuto)
        {
            if (collision.gameObject.name == "Player")
                collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_isAuto)
        {
            if (collision.gameObject.name == "Player")
                collision.transform.SetParent(null);
        }
    }
    public IEnumerator AutomaticMoveCoroutine()
    {
        WaitForSeconds _stopTime = new WaitForSeconds(_stopDelay + moveTime);
        while (true)
        {
            MoveToOffsetPosistion(true);
            yield return _stopTime;
            MoveToOriginPosition(true);
            yield return _stopTime;
        }
    }
    public virtual void MoveToOffsetPosistion(bool isStart = false)
    {
        seq = DOTween.Sequence();
        Vector2 endVec = new Vector2(originVector.x + offsetX, originVector.y + offsetY);
        seq.Append(transform.DOMove(endVec, moveTime));

        moveParticle?.Play();

        if (!isStart)
            SoundManager.Instance.PlaySound(AudioType.EffectSound, moveSound);
    }
    public virtual void MoveToOriginPosition(bool isStart = false)
    {
        seq = DOTween.Sequence();
        seq.Append(transform.DOMove(originVector, moveTime));

        moveParticle?.Play();

        if (!isStart)
            SoundManager.Instance.PlaySound(AudioType.EffectSound, moveSound);
    }

    //혹시 몰라서 
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
