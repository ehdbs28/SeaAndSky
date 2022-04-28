using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private List<AudioClip> effectSounds = null;
    [SerializeField] private List<AudioClip> bgms = null;
    [SerializeField] private List<AudioClip> easterEggEffectSounds = null;
    private AudioSource bgmAudio;
    private AudioSource effectAudio;
    private AudioSource effectAudio2;
    private AudioSource effectAudio3;

    private void Awake()
    {
        SoundManager[] smanagers = FindObjectsOfType<SoundManager>();
        if (smanagers.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        bgmAudio = GetComponent<AudioSource>();
        effectAudio = transform.GetChild(0).GetComponent<AudioSource>();
        effectAudio2 = transform.GetChild(1).GetComponent<AudioSource>();
        effectAudio3 = transform.GetChild(2).GetComponent<AudioSource>();
    }

    private void Start()
    {
        VolumeSetting();
    }

    public void VolumeSetting()
    {
        bgmAudio.mute = false;
        effectAudio.mute = false;
        effectAudio2.mute = false;
        effectAudio3.mute = false;
    }

    public void BGMVolume(float value)
    {
        if (bgmAudio == null) return;
        bgmAudio.volume = value;
    }

    public void BGMMute(bool isMute)
    {
        bgmAudio.mute = isMute;
    }
    public void EffectMute(bool isMute)
    {
        effectAudio.mute = isMute;
    }

    public void EffectVolume(float value)
    {
        if (effectAudio == null) return;
        effectAudio.volume = value;
    }
    public void SetBGM(int bgmNum)
    {
        bgmAudio.Stop();
        bgmAudio.clip = bgms[bgmNum];
        bgmAudio.Play();
    }
    public void SetEffectSound(int effectNum)
    {
        effectAudio.Stop();
        effectAudio.clip = effectSounds[effectNum];
        effectAudio.Play();
    }

    public void SetEffectSound2(int effectNum)
    {
        effectAudio2.Stop();
        effectAudio2.clip = effectSounds[effectNum];
        effectAudio2.Play();
    }

    public void SetEffectSound3(int effectNum)
    {
        effectAudio3.Stop();
        effectAudio3.clip = effectSounds[effectNum];
        effectAudio3.Play();
    }

    public void SetEsterEggEffectSound(int effectNum)
    {
        effectAudio.Stop();
        effectAudio.clip = easterEggEffectSounds[effectNum];
        effectAudio.Play();
    }
    public void StopBGM()
    {
        bgmAudio.Stop();
    }

    public float GetEffectSoundLength()
    {
        return effectAudio.clip.length;
    }

}