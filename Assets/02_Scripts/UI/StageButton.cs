using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
{
    private Text stageText;
    private Button button;
    int index;

    public void Init(int index)
    {
        stageText ??= GetComponentInChildren<Text>();
        button ??= GetComponent<Button>();
        this.index = index;

        button.onClick.AddListener(() =>
        {
            DataManager.Instance.User.stage = this.index;
            //GameManager.Instance.sceneManager.LoadScene("doyun");
            SceneManager.LoadScene("Main");
        });

        stageText.text = index.ToString();

        if (DataManager.Instance.User.maxStage < index)
        {
            button.interactable = false;
        }
    }
}
