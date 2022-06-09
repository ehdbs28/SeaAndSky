using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private int _heartCnt;
    [SerializeField] private GameObject _heartPrefab;
    [SerializeField] private Transform _parentTrm;

    private List<GameObject> heartList = new List<GameObject>();

    private void Start()
    {
        for(int i = 0; i < _heartCnt; i++)
        {
            GameObject heart = Instantiate(_heartPrefab);
            heart.transform.SetParent(_parentTrm);
            heartList.Add(heart);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ReduceHeart();
        }
    }

    public void ReduceHeart()
    {
        GameObject lastIndex = heartList[heartList.Count - 1];
        Sequence sq = DOTween.Sequence();

        sq.Append(lastIndex.transform.DOScale(new Vector3(2.2f, 2.2f, 2.2f), 0.2f));
        sq.Append(lastIndex.transform.DOScale(new Vector3(0, 0, 0), 0.5f));
        sq.OnComplete(() =>
        {
            Destroy(lastIndex);
        });

        heartList.RemoveAt(heartList.Count - 1);
    }
}
