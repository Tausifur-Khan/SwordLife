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
        
        [Header("Box Collider Array")]
        public GameObject attackCol;
        public float playerGroundDmg;
 

        //public Keycode
        public KeyCode attack = KeyCode.I;
        public KeyCode jumpAttack = KeyCode.I;

        //private variable components
        private Animator anim;

        //private variable for CharControl script
        private PlayerController charC;
       
        
        // Start is called before the first frame update
        void Start()
        {
      

            //box colliders enabled false
            //attackCol.GetComponent<BoxCollider2D>().enabled = false;
            attackCol.SetActive(false);

            //refer to Animator component
            anim = GetComponent<Animator>();
            //refer to CharControl component
            charC = GetComponent<PlayerController>();


        }

        // Update is called once per frame
        void Update()
        {
            AttackInput();
           
        }

        //Attack Input Method
        void AttackInput()
        {
            //Ground Attack
            #region GroundAttack
            //if attack bool condition is true

            //if input key && grounded then...
            if (Input.GetKeyDown(attack) && (charC.groundCheck[0] || charC.groundCheck[1]))
            {
                //trigger attack animation
                anim.SetTrigger("isAttacking");
            }

            #endregion

            //Jump Attack
            #region JumpAttack

            //if input key and not grounded then...
            if (Input.GetKeyDown(jumpAttack) && (!(charC.groundCheck[0] && charC.groundCheck[1])))
            {
                //set jump attack animation
                anim.SetTrigger("isJumpAttack");
            }

            #endregion

        }

        //Attack Animation Event
        void GroundEvent()
        {
            //box colliders are enabled
            //Set if statements dependent on sprite direction
            if (charC.sprite.flipX == false)
            {
                //change gameobject localscale to new localscale in x by +1
                //Mathf.Sign to ensure value is set to 1 in x
                attackCol.transform.localPosition = new Vector2(1f, 0);
                //set col true

                //attackCol.GetComponent<BoxCollider2D>().enabled = true;
                attackCol.SetActive(true);


            }
            else if (charC.sprite.flipX == true)
            {
                //set gameobjects localscale to new localscale in x by -1
                //Mathf.Sign to ensure value is -1 in x
                attackCol.transform.localPosition = new Vector2( -1f,0);
                //set col true


                //attackCol.GetComponent<BoxCollider2D>().enabled = true;
                attackCol.SetActive(true);
            }

        }

        void JumpEvent()
        {
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

        }
        //Disable attack collider in animation event
        void DisableCollder()
        {
            //set collider false
            //attackCol.GetComponent<BoxCollider2D>().enabled = false;
            attackCol.SetActive(false);
        }


    }
}
