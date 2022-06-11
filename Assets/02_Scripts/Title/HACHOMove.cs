using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HACHOMove : MonoBehaviour
{
    [SerializeField] private GameObject _hachoR;
    [SerializeField] private GameObject _hachoL;

    private Sequence sq;

    private void Start()
    {
        ShakeHacho();
    }

    public void ShakeHacho()
    {
        sq = DOTween.Sequence();
        sq.Append(_hachoR.transform.DORotate(new Vector3(0, 0, -45), 1f));
        sq.Join(_hachoL.transform.DORotate(new Vector3(0, 0, 45), 1f));

        sq.Append(_hachoR.transform.DORotate(new Vector3(0, 0, -30), 1f));
        sq.Join(_hachoL.transform.DORotate(new Vector3(0, 0, 30), 1f));
        sq.AppendCallback(() =>
        {
            ShakeHacho();
        });
    }
}
