using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Monster _monster;

    public UnityEvent<Vector2> OnEnemyRespawn;
    private void Awake()
    {
        _monster = GetComponentInChildren<Monster>();
    }
    public void Respawn()
    {
        Sequence seq = DOTween.Sequence();
        _monster.gameObject.SetActive(true);
        seq.Append(_monster._SpriteRenderer.material.DOFade(1, 1f));

        OnEnemyRespawn?.Invoke(transform.position);
    }
    public void Dead()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_monster._SpriteRenderer.material.DOFade(0, 1f));
        seq.OnComplete(() => { _monster.gameObject.SetActive(false); });

        Invoke("Respawn", _monster._deadTime);
    }
}
