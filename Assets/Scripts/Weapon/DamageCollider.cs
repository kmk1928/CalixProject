using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    BoxCollider damageCollider;
    // Start is called before the first frame update
    private void Awake() {
        damageCollider = GetComponent<BoxCollider>();
    }

    public void EnableMeleeArea() {
        damageCollider.enabled = true;
    }
    public void DisableMeleeArea() {
        damageCollider.enabled = false;
    }

}
