using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgm_Source;
    public AudioSource playerEffectSource;

    public void SetBgmVolume(float volume)
    {
        bgm_Source.volume = volume;
    }

    public void SetEffectVolume(float volume)
    {
        playerEffectSource.volume = volume;
    }
}