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
    // �߰�: ��ƼŬ ������
    public GameObject particleEffectPrefab;
    // �߰�: ���� �޺� ������ ������ AttackPattern
    public SOAttackPattern nextComboAttack;
}
