using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticleActiveCollider : MonoBehaviour
{
    BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void ColliderEnable() {
        StartCoroutine(collider_C());
    }

    IEnumerator collider_C() {
        boxCollider.enabled = true;
        yield return new WaitForSeconds(0.3f);
        boxCollider.enabled = false;
    }
}
