using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SwordEffect : MonoBehaviour
{
    public ParticleSystem[] swordEffect;
    
    IEnumerator DefaultAttack1() {
        swordEffect[0].Play();
        yield return new WaitForSeconds(0.5f);
        swordEffect[0].Stop();
    }
    IEnumerator DefaultAttack2() {
        swordEffect[1].Play();
        yield return new WaitForSeconds(0.5f);
        swordEffect[1].Stop();
    }
}
