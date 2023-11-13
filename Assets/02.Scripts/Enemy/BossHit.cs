using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHit : EnemyHit
{
    public GameObject BossDieParticle;

    private void BossDie_Pc()
    {
            GameObject particleInstance = Instantiate(BossDieParticle, this.transform.position, this.transform.rotation);
            Destroy(particleInstance, 3f);

            GameManager.isGameclear = true;
            GameManager.instance.GameClear();
    }
}
