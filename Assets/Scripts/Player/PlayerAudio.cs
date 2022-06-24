using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _collectedItemClip;
    [SerializeField] private AudioClip _collectedMoneyClip;
    [SerializeField] private AudioClip _soldClip;
    [SerializeField] private AudioClip _lostClip;

    public void PlayCollectedItemClip()
    {
        _audioSource.PlayOneShot(_collectedItemClip);
    }

    public void PlayCollectedMoeyClip()
    {
        _audioSource.PlayOneShot(_collectedMoneyClip);
    }

    public void PlaySoldClip()
    {
        _audioSource.PlayOneShot(_soldClip);
    }

    public void PlayLostClip()
    {
        _audioSource.PlayOneShot(_lostClip);
    }
}
