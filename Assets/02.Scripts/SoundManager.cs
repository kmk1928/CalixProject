using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    AudioSource EffectSource;
    public AudioSource bgm_Source;
    
    //플레이어
    public AudioClip playerFootstep;
    public AudioClip playerAttack;
    public AudioClip jump;
    public AudioClip ilsum;
    public AudioClip dohon;
    //보스
    public AudioClip bossFootstep;
    public AudioClip bossAttack;
    //해골
    public AudioClip skeletonFootstep;
    public AudioClip skeletonAttack;
    //미노타우로스
    public AudioClip minotaurosFootstep;
    public AudioClip minotaurosAttack;
    //오크
    public AudioClip orcFootstep;
    public AudioClip orcAttack;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
    }

    public void Start()
    {
        EffectSource = GetComponent<AudioSource>();
    }

    public void SetBgmVolume(float volume)
    {
        bgm_Source.volume = volume;
    }

    public void SetEffectVolume(float volume)
    {
        EffectSource.volume = volume;
    }
    
    //플레이어
    public void PlayerAttackSound()
    {
        EffectSource.PlayOneShot(playerAttack);
    }
    public void PlayerFootstepSound()
    {
        EffectSource.PlayOneShot(playerFootstep);
    }
    public void JumpSound()
    {
        EffectSource.PlayOneShot(jump);
    }
    public void IlsumSound()
    {
        EffectSource.PlayOneShot(ilsum);
    }
    public void DohonSound()
    {
        EffectSource.PlayOneShot(dohon);
    }
    //보스
    public void BossAttackSound()
    {
        EffectSource.PlayOneShot(bossAttack);
    }
    public void BossFootstepSound()
    {
        EffectSource.PlayOneShot(bossFootstep);
    }
    //해골
    public void SkeletonAttackSound()
    {
        EffectSource.PlayOneShot(skeletonAttack);
    }
    public void SkeletonFootstepSound()
    {
        EffectSource.PlayOneShot(skeletonFootstep);
    }
    //미노타우로스
    public void MinotaurosAttackSound()
    {
        EffectSource.PlayOneShot(minotaurosAttack);
    }
    public void MinotaurosFootstepSound()
    {
        EffectSource.PlayOneShot(minotaurosFootstep);
    }
    //오크
    public void OrcAttackSound()
    {
        EffectSource.PlayOneShot(orcAttack);
    }
    public void OrcFootstepSound()
    {
        EffectSource.PlayOneShot(orcFootstep);
    }

}


    
