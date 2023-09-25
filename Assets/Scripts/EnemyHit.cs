using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public int maxHP = 100;           //�ִ�ü��
    public int curHP = 100;           //����ü��

    //Rigidbody rigid;            //�ǰ�Ȯ���� ����
    BoxCollider boxCollider;    //�ǰ�Ȯ���� ����
    Material mat;               //�ǰ� �� ���󺯰� Ȯ��

    private void Awake() {
        //rigid = GetComponent<Rigidbody>();              //�ǰ�Ȯ���� ����
        boxCollider = GetComponent<BoxCollider>();      //�ǰ�Ȯ���� ����\
        mat = GetComponentInChildren<MeshRenderer>().material;    //�ǰ� �� ���󺯰� Ȯ��
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Melee") {  //�±װ� Melee�϶� ���
            curHP -= 10;
            Debug.Log("Enemy Hit!! curHP = " + curHP);
            StartCoroutine(OnDamage());
        }
    }

    IEnumerator OnDamage() {
        mat.color = Color.red;                  //�ǰ� �� ���������� ���� �� 
        yield return new WaitForSeconds(0.1f);  // 0.1�� �� �Ʒ� ���ǹ��� ���� �� ���� 

        if (curHP > 0) {
            mat.color = Color.white;        //ü���� ���������� �������
        }
        else {
            mat.color = Color.gray;
            // Destroy(gameObject, 4);      //ü���� ������ ȸ�� + 4�� �� ����
        }
    }// Start is called before the first frame update
}
