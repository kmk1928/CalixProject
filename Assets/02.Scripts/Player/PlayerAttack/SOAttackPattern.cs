using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackPattern", menuName = "PlayerAttack/Attack Pattern")]
public class SOAttackPattern : ScriptableObject {
    public string attackName;
    public float damageMultiplier = 1.0f;  //데미지 계수
    public float cooldown = 0.4f;          //연속입력 방지시간 ex) 0.3f면 애니메이션 실행 후 0.3초 이후에 다음 입력가능
    [Tooltip("애니메이션의 몇 프레임에 공격 콜라이더를 활성화 할건지")] //0.01은 1프레임 0.1은 10프레임
    public float attackCollider_ActiveTime = 0.2f;
    public float hardness_Damage = 10;

    public AnimatorOverrideController animatorOV;

    public string AnimTag;

    [Header("파티클 관련")]
    public GameObject particleEffectPrefab;
    public Vector3 particlePosition;
    public Vector3 particleRotation;
    public Vector3 particleScale = Vector3.one;
    [Tooltip("애니메이션 시작 후 몇 초 뒤에 파티클을 생성할 지")]
    public float particleStartTime = 0.1f;
    [Tooltip("파티클을 생성 후 제거까지 걸리는 시간")]
    public float particleEndTime = 1f;

}
