using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    BoxCollider damageCollider;
    // Start is called before the first frame update
    private void Awake() {
        damageCollider = GetComponent<BoxCollider>();   //�ڽ��� �ڽ��ݶ��̴� (���ݹ���) ����
    }

    public void EnableMeleeArea() {     //���ݹ��� �ݶ��̴� On
        damageCollider.enabled = true;
    }
    public void DisableMeleeArea() {    //���ݹ��� �ݶ��̴� Off
        damageCollider.enabled = false;
    }

}
