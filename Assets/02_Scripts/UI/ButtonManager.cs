using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private  Image _gameStart;

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

    public void SceneChange(string name)
    {
        //메인씬으로 체인지 하는 코드 넣어주세요 .
    }
}
