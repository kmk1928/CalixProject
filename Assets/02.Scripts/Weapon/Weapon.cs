using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type{Melee, Range};
    public Type type;
    public int damage;
    //public float rate;
    //public BoxCollider meleeArea;


    //public void EnableMeleeArea() {
    //    meleeArea.enabled = true;
    //}
    //public void DisableMeleeArea() {
    //    meleeArea.enabled = false;
    //}
    

    /*
    public void Use()
    {
        if(type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
        
    }
    
    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(1.0f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
    }
    */
}
