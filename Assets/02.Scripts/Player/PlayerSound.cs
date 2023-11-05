using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
   // Animator 컴포넌트
   Animator anim;
   
   // 발소리 오디오 클릭
   public AudioClip footstep;
   public AudioClip jump;
   public AudioClip attack;
   public AudioClip skill;
   public AudioClip skill1;
   
   // Start is called before the first frame update
   void Start()
   {
      // Animator 컴포넌트 가져오기
      anim = GetComponent<Animator>();
   }

   // Update is called once per frame
   void Update()
   {
   }

   // 애니메이션 이벤트
   void FootStep()
   {
      // 이벤트가 발생하면 사운드 재생
      AudioSource.PlayClipAtPoint(footstep, Camera.main.transform.position);
   }
   void Jump()
   {
      // 이벤트가 발생하면 사운드 재생
      AudioSource.PlayClipAtPoint(jump, Camera.main.transform.position);
   }
   void Sound_Attack_normal()
   {
      // 이벤트가 발생하면 사운드 재생
      AudioSource.PlayClipAtPoint(attack, Camera.main.transform.position);
   }
   void Skill_A_Sound()
   {
      // 이벤트가 발생하면 사운드 재생
      AudioSource.PlayClipAtPoint(attack, Camera.main.transform.position);
   }
   void Skill_G_Sound()
   {
      // 이벤트가 발생하면 사운드 재생
      AudioSource.PlayClipAtPoint(attack, Camera.main.transform.position);
   }
   void Fly_Attack_Sound()
   {
      // 이벤트가 발생하면 사운드 재생
      AudioSource.PlayClipAtPoint(attack, Camera.main.transform.position);
   }
   void BloodRain_Sound()
   {
      // 이벤트가 발생하면 사운드 재생
      AudioSource.PlayClipAtPoint(skill, Camera.main.transform.position);
   }
   void  BloodRain_Sound1()
   {
      // 이벤트가 발생하면 사운드 재생
      AudioSource.PlayClipAtPoint(attack, Camera.main.transform.position);
   }
   void  OneSlashSound()
   {
      // 이벤트가 발생하면 사운드 재생
      AudioSource.PlayClipAtPoint(skill1, Camera.main.transform.position);
   }
}