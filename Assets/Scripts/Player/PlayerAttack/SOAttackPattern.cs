using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackPattern", menuName = "PlayerAttack/Attack Pattern")]
public class SOAttackPattern : ScriptableObject {
    public string attackName;
    public float damageMultiplier = 1.0f;  //������ ���
    public float cooldown = 0.4f;          //�����Է� �����ð� ex) 0.3f�� �ִϸ��̼� ���� �� 0.3�� ���Ŀ� ���� �Է°���
    [Tooltip("�ִϸ��̼��� ���൵�� ��%�϶� ���� �ݶ��̴��� Ȱ��ȭ �Ұ���")]
    public float attackCollider_ActiveTime = 0.25f;

    public AnimatorOverrideController animatorOV;

    [Header("��ƼŬ ����")]
    public GameObject particleEffectPrefab;
    public Vector3 particlePosition;
    public Vector3 particleRotation;
    public Vector3 particleScale = Vector3.one;
    [Tooltip("�ִϸ��̼� ���� �� �� �� �ڿ� ��ƼŬ�� ������ ��")]
    public float particleStartTime = 0.1f;
    [Tooltip("��ƼŬ�� ���� �� ���ű��� �ɸ��� �ð�")]
    public float particleEndTime = 1f;

}
