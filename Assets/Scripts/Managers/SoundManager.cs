using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    AudioSource audioSource;
    [SerializeField] AudioClip goblinDieSound;
    [SerializeField] AudioClip attackSound;
    [SerializeField] AudioClip looseSound;
    [SerializeField] AudioClip winSound;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayGoblinDieSound()
    {
        audioSource.PlayOneShot(goblinDieSound);
    }

    public void PlayAttackSound()
    {
        audioSource.PlayOneShot(attackSound);
    }

    public void PlayLooseSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(looseSound);
    }

    public void PlayWinSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(winSound);
    }
}
