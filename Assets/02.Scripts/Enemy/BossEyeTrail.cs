using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEyeTrail : MonoBehaviour
{
    TrailRenderer trailRenderer;
    public float trailDuration = 1.0f; // Ʈ���� ���� �ð�
    public float trailWidth = 0.2f; // Ʈ���� �ʺ�
    public ParticleSystem particle;
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        // Ʈ���� ������ ����
        trailRenderer.time = trailDuration;
        trailRenderer.startWidth = trailWidth;
        trailRenderer.endWidth = 0.0f; // �� �κ��� ��� �����Ͽ� ���̵� �ƿ� ȿ���� ����ϴ�.

    }

}
