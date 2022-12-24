using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class CutManager : MonoBehaviour
{
    public bool isSkip = true;

    public PlayableDirector director;

    public void Play()
    {
        Debug.Log("p");
        director.Play();
    }

    private void Update()
    {
        if(isSkip && Input.anyKeyDown)
        {
            isSkip = false;
            SceneChangeManager.Instance.LoadScene("MTItle");
        }

        if(director.time >= 40)
        {
            SceneManager.LoadScene("MTItle");
        }
    }
}
