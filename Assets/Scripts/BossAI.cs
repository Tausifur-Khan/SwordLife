using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : Enemy
{
    public bool isAttacking;
    public int attackMode;
    private float attackCounter;
    public float attackTimer = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(attackCounter <= 0)
        {
            switch (attackMode)
            {
                case 0:
                    attackCounter = 1.2f;
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
            attackCounter += attackTimer;
        }
        attackCounter -= Time.deltaTime;
    }
}
