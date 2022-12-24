using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private AudioClip[] _bgmClips;

    private AudioSource bgmAudio;
    private AudioSource effectSoundAudio;
    private AudioSource natureAudio;

    public void SoundManagerAwake()
    {
        if (FindObjectsOfType<SoundManager>().Length != 1)
        {
            Destroy(gameObject);
            return;
        }

        AudioSource[] sources = GetComponentsInChildren<AudioSource>();

        bgmAudio = sources[0];
        effectSoundAudio = sources[1];
        natureAudio = sources[2];

        //DontDestroyOnLoad(this);
    }

    public void PlayRandomBGM(){
        int clipNum = 0;
        if(DataManager.Instance.User.selectedBGM.Count >= 5)
            DataManager.Instance.User.selectedBGM.Clear();

        while(DataManager.Instance.User.selectedBGM.Contains(clipNum)){
            clipNum = Random.Range(0, _bgmClips.Length);
        }   
        DataManager.Instance.User.selectedBGM.Add(clipNum);
        AudioClip clip = _bgmClips[clipNum];

        bgmAudio.clip = clip;
        bgmAudio.Play();
    }

    public void PlaySound(AudioType audioType, AudioClip clip)
    {
        if (audioType == AudioType.BGM)
        {
            bgmAudio.Stop();
            bgmAudio.clip = clip;
            bgmAudio.Play();
        }

        else if (audioType == AudioType.EffectSound)
        {
            effectSoundAudio.PlayOneShot(clip);
        }

        else if(audioType == AudioType.Nature)
        {
            natureAudio.Stop();
            natureAudio.clip = clip;
            natureAudio.Play();
        }
    }
}

