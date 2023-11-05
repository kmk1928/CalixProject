using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTrail : MonoBehaviour
{
    public MeleeWeaponTrail flymech_Trail;
    public void FlyMech_Trail()
    {
        flymech_Trail._emitTime = 0.2f;
        flymech_Trail.Emit = true;
    }

}
