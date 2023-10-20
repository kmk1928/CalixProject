using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackPattern", menuName = "PlayerAttack/Attack Pattern")]
public class SOAttackPattern : ScriptableObject {
    public string attackName;
    public float damage;
    public float cooldown;
    public float range;
    //public AnimationClip attackAnimation;
    public AnimatorOverrideController animatorOV;
    // 추가: 파티클 프리팹
    public GameObject particleEffectPrefab;
    // 추가: 다음 콤보 공격을 참조할 AttackPattern
    public SOAttackPattern nextComboAttack;
}
