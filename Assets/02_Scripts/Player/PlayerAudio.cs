using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class PlayerAudio : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] private AudioClip skyJumpSound;
    [SerializeField] private AudioClip seaJumpSound;

    [Header("Walk")]
    [SerializeField] private AudioClip skyWalkSound;
    [SerializeField] private AudioClip[] seaWalkSounds;

    [Header("Attack")]
    [SerializeField] private AudioClip attackSound;

    [Header("Change")]
    [SerializeField] private AudioClip seaSound;
    [SerializeField] private AudioClip skySound;

    [Header("Nature Sound")]
    [SerializeField] private AudioClip seaNatureSound;
    [SerializeField] private AudioClip skyNatureSound;


    private float walkTimer = 0f;
    private float WALK_DELAY = 0f;

    private void Start()
    {
        WALK_DELAY = skySound.length;
    }

    private void Update()
    {
        walkTimer += Time.deltaTime;
    }

    public void PlayJumpSound()
    {
        if (GameManager.Instance.PlayerState == AreaState.Sky)
        {
            SoundManager.Instance.PlaySound(AudioType.EffectSound, skyJumpSound);
        }
        else
        {
            SoundManager.Instance.PlaySound(AudioType.EffectSound, seaJumpSound);
        }
    }

    public void PlayWalkSound(Vector2 direction)
    {
        if (walkTimer > WALK_DELAY && Mathf.Abs(direction.x) >= 0.5f)
        {
            if (GameManager.Instance.PlayerState == AreaState.Sky && Mathf.Abs(direction.y) < 0.01f)
            {
                SoundManager.Instance.PlaySound(AudioType.EffectSound, skyWalkSound);
            }
            else if(GameManager.Instance.PlayerState == AreaState.Sea && Mathf.Abs(direction.y) < 0.01f)
            {
                SoundManager.Instance.PlaySound(AudioType.EffectSound, seaWalkSounds[Random.Range(0, seaWalkSounds.Length)]);
            }

            walkTimer = 0f;
        }
    }

    public void PlayAttackSound()
    {
        SoundManager.Instance.PlaySound(AudioType.EffectSound, attackSound);
    }

    public void PlayChangeSound(AreaState area)
    {
        if (area == AreaState.Sea)
        {
            SoundManager.Instance.PlaySound(AudioType.EffectSound, seaSound);
            SoundManager.Instance.PlaySound(AudioType.Nature, seaNatureSound);
            WALK_DELAY = Random.Range(0.9f, 1.5f);
        }
        else if (area == AreaState.Sky)
        {
            SoundManager.Instance.PlaySound(AudioType.EffectSound, skySound);
            SoundManager.Instance.PlaySound(AudioType.Nature, skyNatureSound);
            WALK_DELAY = 0.3f;
        }
    }
}