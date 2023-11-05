using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSound : MonoBehaviour
{
   // Animator 컴포넌트
   Animator anim;
   
   // 발소리 오디오 클릭
   public AudioClip footstep;
   public AudioClip attack;
   
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
   void BossAttackSound()
   {
      // 이벤트가 발생하면 사운드 재생
      AudioSource.PlayClipAtPoint(attack, Camera.main.transform.position);
   }
}