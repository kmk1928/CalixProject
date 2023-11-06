using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    AudioSource myAudio;
    public AudioSource bgm_Source;
    public AudioSource playerEffectSource;

    public AudioClip bossFootstep;
    public AudioClip bossAttack;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
    }

    public void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    public void SetBgmVolume(float volume)
    {
        bgm_Source.volume = volume;
    }

    public void SetEffectVolume(float volume)
    {
        playerEffectSource.volume = volume;
    }

    public void BossAttackSound()
    {
        myAudio.PlayOneShot(bossAttack);
    }

    public void BossFootstepSound()
    {
        myAudio.PlayOneShot(bossFootstep);
    }


}


    
