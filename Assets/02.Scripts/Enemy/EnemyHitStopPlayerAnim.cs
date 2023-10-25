using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitStopPlayerAnim : MonoBehaviour
{
    //������
    Animator animator;
    private bool stopping = false;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("melee")) {  //�±װ� Melee�϶� ���
            animator = other.transform.root.GetComponent<Animator>();
            float animSpeed = animator.speed;
            if (!stopping) {
                StartStopAnimCor(animSpeed);
            }
        }
    }

    void StartStopAnimCor(float animSpeed) {
        stopping = true;
        StartCoroutine(StopAnim(animSpeed));
    }
    IEnumerator StopAnim(float animSpeed) {

        animator.speed = 0;
        yield return new WaitForSeconds(0.08f);
        animator.speed = animSpeed;
        stopping = false;
    }
}
