using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class CutManager : MonoBehaviour
{
    public bool isSkip = true;

    public PlayableDirector playableDirector;
    public TimelineAsset timeline;

    private void Update()
    {
        if(isSkip && Input.anyKeyDown)
        {
            isSkip = false;
            SceneManager.LoadScene("MTItle");
        }
    }
    
    public void EndScene()
    {
        SceneManager.LoadScene("MTItle");
    }
}
