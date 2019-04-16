using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knight
{

    public class RangeTimer : MonoBehaviour
    {

        #region Time
        public float timer;
        public float maxTimer;
        #endregion

        private Attack attack;

        private void Start()
        {
            attack = GetComponent<Attack>();
            timer = maxTimer;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            AttackTime();
        }

        void AttackTime()
        {
            #region Ground Attack Time
            //if attack bool condition is false then...
            if (!attack.canRange)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    attack.canRange = true;
                    timer = maxTimer;
                }
            }
            #endregion
        }
    }
}
