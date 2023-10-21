using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    BoxCollider damageCollider;
    // Start is called before the first frame update
    private void Awake() {
        damageCollider = GetComponent<BoxCollider>();   //자신의 박스콜라이더 (공격범위) 받음
    }

    public void EnableMeleeArea() {     //공격범위 콜라이더 On
        damageCollider.enabled = true;
    }
    public void DisableMeleeArea() {    //공격범위 콜라이더 Off
        damageCollider.enabled = false;
    }

}
