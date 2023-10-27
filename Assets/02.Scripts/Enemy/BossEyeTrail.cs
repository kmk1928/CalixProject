using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEyeTrail : MonoBehaviour
{
    TrailRenderer trailRenderer;
    //public float trailDuration = 0.5f; // Ʈ���� ���� �ð�
    //public float trailWidth = 0.01f; // Ʈ���� �ʺ�
    public ParticleSystem particle_red;
    public ParticleSystem particle_distortion;
    public Light eyeLight;
    void Start()
    {
        eyeLight = GetComponentInChildren<Light>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        //// Ʈ���� ������ ����
        //trailRenderer.time = trailDuration;
        //trailRenderer.startWidth = trailWidth;
        //trailRenderer.endWidth = 0.0f; // �� �κ��� ��� �����Ͽ� ���̵� �ƿ� ȿ���� ����ϴ�.

    }

    public void EyeParticleEnable() 
    {
        eyeLight.enabled = true;
        particle_distortion.Play();
        particle_red.Play();
        trailRenderer.enabled = true;
    }

    public void EyeParticleDisable() {
        eyeLight.enabled = false;        
        trailRenderer.enabled = false;
    }
}
