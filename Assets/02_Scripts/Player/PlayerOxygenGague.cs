using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerOxygenGague : MonoBehaviour
{
    [SerializeField] private float _maxLifeTime = 10f;
    private GameObject _gagueBox;
    private GameObject _gauge;
    private bool _isSea = false;
    private float _lifeTime = 0;
    private PlayerMove _player;
    private void Awake()
    {
        _gagueBox = GameObject.Find("OxygenGagueBox");
        _gauge = transform.Find("OxygenGague").gameObject;
        _player = transform.parent.GetComponent<PlayerMove>();
        CheckSea();
    }

    public void CheckSea()
    {
        _isSea = GameManager.Instance.PlayerState == AreaState.Sea ? true : false;

        CheckShowGague(_isSea);
    }


    public void CheckShowGague(bool value)
    {
        _lifeTime = _maxLifeTime;
        _isSea = value;
        _gagueBox.SetActive(value);
        _gauge.SetActive(value);
    }


    private void FixedUpdate()
    {
        if (_isSea)
        {
            if (_lifeTime <= 0)
            {
                _player.Damage();
                return;
            }
            _lifeTime -= Time.fixedDeltaTime;
            _gauge.transform.DOScaleX(_lifeTime / _maxLifeTime, 0.01f);
        }
    }
}
