using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitStopEnemyAnim : MonoBehaviour
{
    //�Ϲݸ���
    Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private bool stopping = false;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("melee")) {  //�±װ� Melee�϶� ���
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
