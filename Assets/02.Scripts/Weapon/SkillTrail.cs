using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTrail : MonoBehaviour
{
    public MeleeWeaponTrail flymech_Trail;
    public void FlyMech_Trail()
    {
        if (!GameManager.isFlyMechTrail)
        {
            flymech_Trail.MeleeTrail_StartFunc();
            GameManager.isFlyMechTrail = true;
        }
        flymech_Trail._emitTime = 0.2f;
        flymech_Trail.Emit = true;
    }

}
