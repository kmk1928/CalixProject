using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public Transform player; // 플레이어 게임 오브젝트
    Animator anim; // animation 변수

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); // animation
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (6 < distanceToPlayer && distanceToPlayer <= 8) // 플레이어 발견 시
            {
                anim.SetBool("Greeting", true); // SetBool 메서드로 수정
            }
            else
            {
                anim.SetBool("Greeting", false); // else 블록에서 false로 설정하여 애니메이션을 끄세요.
            }
        }
    }
}