using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
   // Animator 컴포넌트
   Animator anim;
   
   void Start()
   {
      // Animator 컴포넌트 가져오기
      anim = GetComponent<Animator>();
   }
   
   // 애니메이션 이벤트
   void FootStep()
   {
      // 이벤트가 발생하면 사운드 재생
      SoundManager.instance.PlayerFootstepSound();
   }
   void Jump()
   {
      // 이벤트가 발생하면 사운드 재생
      SoundManager.instance.JumpSound();
   }
   void Sound_Attack_normal()
   {
      // 이벤트가 발생하면 사운드 재생
      SoundManager.instance.PlayerAttackSound();
   }
   void Skill_A_Sound()
   {
      // 이벤트가 발생하면 사운드 재생
      SoundManager.instance.PlayerAttackSound();
   }
   void Skill_G_Sound()
   {
      // 이벤트가 발생하면 사운드 재생
      SoundManager.instance.PlayerAttackSound();
   }
   void Fly_Attack_Sound()
   {
      // 이벤트가 발생하면 사운드 재생
      SoundManager.instance.PlayerAttackSound();
   }
   void BloodRain_Sound()
   {
      // 이벤트가 발생하면 사운드 재생
      SoundManager.instance.DohonSound();
   }
   void  BloodRain_Sound1()
   {
      // 이벤트가 발생하면 사운드 재생
      SoundManager.instance.PlayerAttackSound();
   }
   void  OneSlashSound()
   {
      // 이벤트가 발생하면 사운드 재생
      SoundManager.instance.IlsumSound();
   }
}