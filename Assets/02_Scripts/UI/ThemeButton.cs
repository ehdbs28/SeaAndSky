using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ThemeButton : MonoBehaviour
{
    [SerializeField] private CanvasGroup _levelGroup;
    private CanvasGroup _thisGroup;

    private int _index;
    private Button _button;

    private void Awake() {
        _button ??= GetComponent<Button>();
        _thisGroup ??= transform.parent.parent.GetComponent<CanvasGroup>();
        _index = int.Parse(gameObject.name.Replace("Theme", ""));
    }

    public void Init(){
        if(DataManager.Instance.User.theme < _index){
            _button.interactable = false;
        }

        _button.onClick.AddListener(() => {
            _thisGroup.DOFade(0f, 1f).OnComplete(() => { _thisGroup.interactable = false; _thisGroup.blocksRaycasts = false; });
            _levelGroup.DOFade(1, 1f).OnComplete(() => { _levelGroup.interactable = true; _levelGroup.blocksRaycasts = true; });
        });
    }
}
