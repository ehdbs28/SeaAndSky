using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
{
    private Text stageText;
    private Button button;

    public void Init(int index)
    {
        stageText ??= GetComponentInChildren<Text>();
        button ??= GetComponent<Button>();
        button.onClick.AddListener(() => SceneManager.LoadScene("Main"));

        stageText.text = index.ToString();

        if(DataManager.Instance.User.maxStage < index)
        {
            button.interactable = false;
        }
    }
}
