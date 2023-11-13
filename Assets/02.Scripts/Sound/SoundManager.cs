using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    AudioSource EffectSource;
    public AudioClip stage1BGM;
    public AudioClip stage2BGM;
    public AudioClip stage3BGM;
    public AudioClip stage4BGM;
    public AudioClip stage5BGM;
    public AudioClip stageBonusBGM;
    public AudioClip stageShopBGM;
    public AudioClip stageBossBGM;

    private AudioSource bgmSource;
    
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
    //곰
    public AudioClip bearFootstep;
    public AudioClip bearAttack;
    //기사
    public AudioClip knightFootstep;
    public AudioClip knightAttack;
    //원거리
    public AudioClip archerFootstep;
    public AudioClip archerAttack;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); //씬 옮겨도 노파괴
            bgmSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        EffectSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        PlaySceneBGM(); // 초기 씬에 배경 음악을 플레이할 수도 있습니다.
    }

    public void SetBgmVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    public void SetEffectVolume(float volume)
    {
        EffectSource.volume = volume;
    }

    // 씬 전환 시 호출되는 이벤트 핸들러
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlaySceneBGM();
    }

    // 씬에 따른 배경 음악 재생
    private void PlaySceneBGM()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        // 씬 이름에 따라 다른 배경 음악을 설정
        switch (sceneName)
        {
            case "Stage1":
                bgmSource.clip = stage1BGM;
                break;
            case "Stage2":
                bgmSource.clip = stage2BGM;
                break;
            case "Stage3_1":
                bgmSource.clip = stage3BGM;
                break;
            case "Stage3_2":
                bgmSource.clip = stageBonusBGM;
                break;
            case "Stage3_3":
                bgmSource.clip = stage3BGM;
                break;
            case "Stage4_1":
                bgmSource.clip = stage4BGM;
                break;
            case "Stage4_2":
                bgmSource.clip = stageBonusBGM;
                break;
            case "Stage4_3":
                bgmSource.clip = stage4BGM;
                break;
            case "Stage5":
                bgmSource.clip = stage5BGM;
                break;
            case "Stage6_shop":
                bgmSource.clip = stageShopBGM;
                break;
            case "Stage7_Boss":
                bgmSource.clip = stageBossBGM;
                break;
                
            // 추가 씬에 대한 처리 추가...
        }
        bgmSource.Play();
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
    //곰
    public void BearAttackSound()
    {
        EffectSource.PlayOneShot(bearAttack);
    }
    public void BearFootstepSound()
    {
        EffectSource.PlayOneShot(bearFootstep);
    }
    //기사
    public void KnightAttackSound()
    {
        EffectSource.PlayOneShot(knightAttack);
    }
    public void KnightFootstepSound()
    {
        EffectSource.PlayOneShot(knightFootstep);
    }
    //원거리
    public void ArcherAttackSound()
    {
        EffectSource.PlayOneShot(archerAttack);
    }
    public void ArcherFootstepSound()
    {
        EffectSource.PlayOneShot(archerFootstep);
    }
}


    
