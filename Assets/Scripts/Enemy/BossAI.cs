using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : Enemy
{
    public Animator anim;
    public Rocks rockSpawn;
    public bool isAttacking;
    public int attackMode;
    private float attackCounter;
    public float attackTimer = 1;

    // Update is called once per frame
    void Update()
    {
        if(attackCounter <= 0)
        {
            switch (attackMode)
            {
                case 0:
                    anim.Play("BossAttack");
                    attackCounter = 2f;
                    break;
                case 1:
                    anim.Play("BossAttack2");
                    rockSpawn.Invoke("Spawn", 0.9f);
                    attackCounter = 1f;
                    break;
                case 2:
                    break;
            }
            attackMode++;
            if (attackMode > 1) attackMode = 0;
            attackCounter += attackTimer;
        }
        attackCounter -= Time.deltaTime;
    }
}
