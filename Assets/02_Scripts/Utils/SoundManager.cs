using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    private AudioSource bgmAudio;
    private AudioSource effectSoundAudio;
    private AudioSource natureAudio;

    private void Awake()
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

        DontDestroyOnLoad(this);
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

