using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAreaSetup : MeleeAreaSetup
{

    public DamageCollider oneSlash;
    public DamageCollider flymech;
    public DamageCollider bloodRain;
    void OneSlashAttack1()
    {
        StartCoroutine(OneSlashAttack1_C());
    }

    IEnumerator OneSlashAttack1_C()
    {
        oneSlash.EnableMeleeArea();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("ÄÑÁ³´Ù ²¨Áü");
        oneSlash.DisableMeleeArea();
    }

    void FlyMechAttack1_2()
    {
        StartCoroutine(FlyMechAttack1_2_C());
    }

    IEnumerator FlyMechAttack1_2_C()
    {
        flymech.EnableMeleeArea();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("°øÁß ÄÑÁ³´Ù ²¨Áü");
        flymech.DisableMeleeArea();
    }

    void BloodRainAttackArea()
    {
        StartCoroutine(BloodRainAttackArea_C());
    }

    IEnumerator BloodRainAttackArea_C()
    {
        bloodRain.EnableMeleeArea();
        yield return new WaitForSeconds(0.05f);
        Debug.Log("ÄÑÁ³´Ù ²¨Áü");
        bloodRain.DisableMeleeArea();
    }
}
