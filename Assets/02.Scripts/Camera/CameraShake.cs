using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private float m_roughness = 5f;      //거칠기 정도
    [SerializeField]
    private float m_magnitude = 5f;      //움직임 범위

    private void Update() {
        if (Input.GetKeyDown(KeyCode.C)) {
            StartCoroutine(Shake(1f));
        }
    }

    IEnumerator Shake(float duration) {
        float halfDuration = duration / 2;
        float elapsed = 0f;
        float tick = Random.Range(-10f, 10f);

        while (elapsed < duration) {
            elapsed += Time.deltaTime / halfDuration;

            tick += Time.deltaTime * m_roughness;
            transform.position = new Vector3(
                Mathf.PerlinNoise(tick, 0) - .5f,
                Mathf.PerlinNoise(0, tick) - .5f,
                0f) * m_magnitude * Mathf.PingPong(elapsed, halfDuration);

            yield return null;
        }
    }
}
