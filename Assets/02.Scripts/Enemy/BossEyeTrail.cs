using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEyeTrail : MonoBehaviour
{
    TrailRenderer trailRenderer;
    public float trailDuration = 1.0f; // 트레일 지속 시간
    public float trailWidth = 0.2f; // 트레일 너비
    public ParticleSystem particle;
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        // 트레일 렌더러 설정
        trailRenderer.time = trailDuration;
        trailRenderer.startWidth = trailWidth;
        trailRenderer.endWidth = 0.0f; // 끝 부분이 얇게 설정하여 페이드 아웃 효과를 만듭니다.

    }

}
