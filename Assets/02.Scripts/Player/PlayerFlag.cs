using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlag : MonoBehaviour
{

    public static bool isAttacking = false;
    public static bool isInteracting = false;

    public void IsAttacking_To_true() {
        isAttacking = true;
    }
    public void IsAttacking_To_fasle() {
        isAttacking = false;
    }

    public void IsInteracting_To_true() {
        isInteracting = true;
    }
    public void IsInteracting_To_false() {
        isInteracting = true;
    }
}
