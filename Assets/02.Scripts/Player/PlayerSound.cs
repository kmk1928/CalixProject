using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
   // Animator 컴포넌트
   Animator anim;
   
   public AudioSource playerAudioSource;

   // 발소리 오디오 클릭
   public AudioClip footstep;
   public AudioClip jump;
   public AudioClip attack;
   public AudioClip skill;
   public AudioClip skill1;
   

   void Start()
   {
      // Animator 컴포넌트 가져오기
      anim = GetComponent<Animator>();

      // 플레이어 오디오 소스 가져오기
      playerAudioSource = GetComponent<AudioSource>();
   }


   // 애니메이션 이벤트
   void FootStep()
   {
      // 이벤트가 발생하면 사운드 재생
      playerAudioSource.PlayOneShot(footstep);
   }
   void Jump()
   {
      // 이벤트가 발생하면 사운드 재생
      playerAudioSource.PlayOneShot(jump);
   }
   void Sound_Attack_normal()
   {
      // 이벤트가 발생하면 사운드 재생
      playerAudioSource.PlayOneShot(attack);
   }
   void Skill_A_Sound()
   {
      // 이벤트가 발생하면 사운드 재생
      playerAudioSource.PlayOneShot(attack);
   }
   void Skill_G_Sound()
   {
      // 이벤트가 발생하면 사운드 재생
      playerAudioSource.PlayOneShot(attack);
   }
   void Fly_Attack_Sound()
   {
      // 이벤트가 발생하면 사운드 재생
      playerAudioSource.PlayOneShot(attack);
   }
   void BloodRain_Sound()
   {
      // 이벤트가 발생하면 사운드 재생
      playerAudioSource.PlayOneShot(skill);
   }
   void  BloodRain_Sound1()
   {
      // 이벤트가 발생하면 사운드 재생
      playerAudioSource.PlayOneShot(attack);
   }
   void  OneSlashSound()
   {
      // 이벤트가 발생하면 사운드 재생
      playerAudioSource.PlayOneShot(skill1);
   }
}