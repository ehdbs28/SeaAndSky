using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private LayerMask teleportableLayerMask;
    [SerializeField] private GameObject outPotal;
    [SerializeField] private ParticleSystem outEffect;
    [SerializeField] private AudioClip clip;

    private Transform targetTrm;
    private Vector3 outDir;
    private Collider2D hit;

    private void Start()
    {
        outDir = outPotal.transform.position;
    }

    private void Update()
    {
        CheckEnterObj();
    }

    private void CheckEnterObj()
    {
        hit = Physics2D.OverlapCircle(transform.position, 0.5f, teleportableLayerMask);
        if (hit != null)
        {
            TeleportTime();
        }
    }

    public void TeleportTime()
    {
        hit.transform.position = outDir;
        outEffect.Play();
        SoundManager.Instance.PlaySound(AudioType.EffectSound, clip);
    }
}
