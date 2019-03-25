using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

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
        public GameObject attackCol ;
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
            attackCol.SetActive(false);
            
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
                //if input key && grounded then...
                if (Input.GetKeyDown(attack) && (charC.groundCheck[0] || charC.groundCheck[0]))
                {
                    //trigger animation
                    anim.SetTrigger("isAttacking");

                    //box colliders are enabled
                    //Set if statements dependent on sprite direction
                    if (charC.sprite.flipX == false)
                    {
                        //change gameobject localscale to new localscale in x by +1
                        //Mathf.Sign to ensure value is set to 1 in x
                        attackCol.transform.localScale = new Vector2(Mathf.Sign(transform.localScale.x * 1), transform.localScale.y);
                        //set col true
                        attackCol.SetActive(true);

                    }
                    else if (charC.sprite.flipX == true)
                    {
                        //set gameobjects localscale to new localscale in x by -1
                        //Mathf.Sign to ensure value is -1 in x
                        attackCol.transform.localScale = new Vector2(Mathf.Sign(transform.localScale.x * -1), transform.localScale.y);
                        //set col true
                        attackCol.SetActive(true);
                    }

                    //bool condition is false
                    canAttack = false;
                }
            }

        }
    }
}
