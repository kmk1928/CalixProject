using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitParticle : MonoBehaviour
{

    public ParticleSystem hitEffect; // ��ƼŬ �ý��� ����
    public ParticleSystem hitBlood;
    Animator animator;

    private void OnCollisionEnter(Collision collision) {
         if (collision.gameObject.CompareTag("melee")) {
            // �浹 ������ �����ɴϴ�.
            ContactPoint contact = collision.GetContact(0);
            Debug.LogWarning("�Ǵ°Ϥ�?");
            Vector3 contactPoint = contact.point;
            Vector3 contactNormal = contact.normal; //�����߰�
            Debug.LogWarning("��ƼŬ ����");

                // ��ƼŬ �ý����� ��ġ�� ������ �����ϰ� ����մϴ�.
                hitBlood.transform.position = contactPoint;
                // ��ƼŬ �ý����� ȸ���� �����մϴ�.
                hitBlood.transform.rotation = Quaternion.LookRotation(contactNormal); // �浹 ���� �������� ȸ��
                hitBlood.Play();
            
                hitEffect.transform.position = contactPoint;
                hitEffect.Play();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("melee")) {  //�±װ� Melee�϶� ���
            Debug.Log("Enemy Hit!!");
            animator = other.transform.root.GetComponent<Animator>();
            float animSpeed = animator.speed;
            StartCoroutine(StopAnim(animSpeed));
        }
    }
    IEnumerator StopAnim(float animSpeed) {
        animator.speed = 0;
        yield return new WaitForSeconds(0.1f);
        animator.speed = animSpeed;
    }

}
