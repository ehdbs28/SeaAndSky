using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutManager : MonoBehaviour
{
    public bool isSkip = true;

    private void Update()
    {
        if(isSkip && Input.anyKeyDown)
        {
            isSkip = false;
            GameManager.Instance.sceneManager.LoadScene("MTItle");
        }
    }
}
