using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSound : MonoBehaviour
{
   // Animator 컴포넌트
   Animator anim;
   
   // Start is called before the first frame update
   void Start()
   {
      // Animator 컴포넌트 가져오기
      anim = GetComponent<Animator>();
   }

   // 애니메이션 이벤트
   void FootStep()
   {
      // 이벤트가 발생하면 사운드 재생
      SoundManager.instance.BossFootstepSound();
   }
   void EneyDamageColliderOn()
   {
      // 이벤트가 발생하면 사운드 재생
      SoundManager.instance.BossAttackSound();
   }
}