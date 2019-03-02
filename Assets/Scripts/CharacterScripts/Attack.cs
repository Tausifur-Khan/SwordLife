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
        public float attackMaxTime = 0.5f;
        public float timer = 0;

        [Header("Box Collider Array")]
        public BoxCollider2D[] colliders = new BoxCollider2D[4];
        //public Keycode
        public KeyCode attack = KeyCode.I;

        //private variable components
        private Animator anim;
        //private variable for CharControl script
        private CharacterController charC;

        // Start is called before the first frame update
        void Start()
        {
            //box colliders enabled false
            colliders[0].enabled = false;
            colliders[1].enabled = false;
            colliders[2].enabled = false;
            colliders[3].enabled = false;
          

            //timer is equal to total attack cooldown
            timer = attackMaxTime;

            //refer to Animator component
            anim = GetComponent<Animator>();
            //refer to CharControl component
            charC = GetComponent<CharacterController>();

            //set attack bool condition true
            canAttack = true;
        }

        // Update is called once per frame
        void Update()
        {
            AttackInput();

        }
        
        //Attack Method
        void AttackInput()
        {
            //if attack bool condition is true
            if (canAttack)
            {
                //if input key then...
                if (Input.GetKeyDown(attack))
                {
                   
                    //trigger animation
                    anim.SetTrigger("isAttacking");

                    //box colliders are enabled
                    //Set if statements dependent on sprite direction
                    if (charC.sprite.flipX == false)
                    {
                        colliders[0].enabled = true;
                        colliders[1].enabled = true;
                    }
                    else if (charC.sprite.flipX == true)
                    {
                        colliders[2].enabled = true;
                        colliders[3].enabled = true;
                    }

                    //bool condition is false
                    canAttack = false;
                }
            }
            else
            {

            }
        }
    }
}
