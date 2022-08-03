using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _masterMixer;
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;

    private float _bgmSound = 1;
    private float _sfxSound = 1;

    private void Start()
    {
        _bgmSound = PlayerPrefs.GetFloat("BGM", 1);
        _sfxSound = PlayerPrefs.GetFloat("SFX", 1);

        _bgmSlider.value = _bgmSound;
        _sfxSlider.value = _sfxSound;
    }

    private void Update()
    {
        _bgmSound = _bgmSlider.value;
        _sfxSound = _sfxSlider.value;
        PlayerPrefs.SetFloat("BGM", _bgmSound);
        PlayerPrefs.SetFloat("SFX", _sfxSound);

        AudioSetting();
    }

    private void AudioSetting()
    {
        if(_bgmSound == 0) 
        {
            _masterMixer.SetFloat("Nature", -80);
            _masterMixer.SetFloat("BGM", -80);
        }
        else
        {
            _masterMixer.SetFloat("Nature", Mathf.Lerp(-40, 0, _bgmSound));
            _masterMixer.SetFloat("BGM", Mathf.Lerp(-40, 0, _bgmSound));
        }

        if (_sfxSound == 0) { _masterMixer.SetFloat("SFX", -80); }
        else { _masterMixer.SetFloat("SFX", Mathf.Lerp(-40, 0, _sfxSound)); }
    }
}
