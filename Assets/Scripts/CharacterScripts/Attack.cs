using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace Knight
{
    public class Attack : MonoBehaviour
    {
        //public bool condition for attack
        //set bool conditon false for default
        public bool canAttack = false;
        public float attackMaxTime = 1;
        public float timer = 0;

        //public Keycode
        public KeyCode attack = KeyCode.I;

        //private variable for animations
        private Animator anim;

        // Start is called before the first frame update
        void Start()
        {
            //start timer = attackMaxTime
            timer = attackMaxTime;
        }

        // Update is called once per frame
        void Update()
        {
            AttackInput();
        }

        void AttackInput()
        {
            if (Input.GetKeyDown(attack))
            {
                Debug.Log(Input.GetKeyDown(attack));
                anim.SetTrigger("isAttacking");
            }
        }
    }
}
