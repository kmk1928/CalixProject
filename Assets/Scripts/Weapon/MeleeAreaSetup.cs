using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAreaSetup : MonoBehaviour
{
    PlayerController playerCtrl;
    DamageCollider damageCollider;

    private void Start() {
        playerCtrl = GetComponentInParent<PlayerController>();
    }

    public void LoadWeaponDamageCollider() {
        damageCollider = playerCtrl.equipWeapon.GetComponent<DamageCollider>();
    }

    private void OpenDamageCollider() {
        damageCollider.EnableMeleeArea();
    }

    private void CloseDamageCollider() {
        damageCollider.DisableMeleeArea();
    }


}
