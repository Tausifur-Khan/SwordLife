using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

namespace knight
{

    public class CharacterController : MonoBehaviour
    {
        #region Movement Variables X & Y
        //float speed 
        public float mvSpeed = 2;
        //Dash speed
        public float dashSpeed = 5;
        //float jumpForce
        public float jumpForce = 50;
        //Jump condition
        public bool grounded;
        //double jump bool condition
        public bool doubleJump;

        #endregion

        #region Variable Components
        //2d rigidbody component
        private Rigidbody2D rb2d;
        //animation
        private Animator anim;
        //sprite renderer
        private SpriteRenderer sprite;
        #endregion


        // Start is called before the first frame update
        void Start()
        {
            doubleJump = false;
            grounded = true;
            //Refer to rigid component
            rb2d = GetComponent<Rigidbody2D>();
            //refer to animation component
            anim = GetComponent<Animator>();
            //refer to sprite renderer component
            sprite = GetComponent<SpriteRenderer>();
        }

        //Method for Jump()
        void Update()
        {
            Jump();
            //Check condition for grounded
            Debug.Log("Grounded: " + grounded);
            Debug.Log("Double: " + doubleJump);
          
        }

        //Method for Move()
        void FixedUpdate()
        {
            Move();
        }

        //Method: Movement in X direction with velocity
        void Move()
        {
            // access to input axis within horizontal section
            float inputx = Input.GetAxis("Horizontal");

            //rigidbody velocity is equal to new vector2 space
            //new vector space with x value , y normal0
            //rb2d.velocity.y will set pos to 0 but wont effect movement
            rb2d.velocity = new Vector2(inputx * mvSpeed, rb2d.velocity.y);
            
            #region Sprite Flip Condition
            // sprite flip
            if (inputx <= -0.1)
            {
                sprite.flipX = true;
            }
            if (inputx >= 0.1)
            {
                sprite.flipX = false;
            }
            #endregion

            #region Animation Condition
            //animation set
            if (inputx >= 0.1 || inputx <= -0.1)
            {
                anim.SetBool("isMoving", true);

            }
            else
            {
                anim.SetBool("isMoving", false);
            }
            #endregion
        }
        
        void Dash()
        {
            //rb2d.velocity.y will set pos to 0 but wont effect movement
            //if key is space then...
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //allow character to dash. No condition required
                Debug.Log("Dash: " + Input.GetKeyDown(KeyCode.Space));

                
                //set cooldown

            }
        }

        //Method: Movement in Y direction with velocity & Animation
        void Jump()
        {
            //if this key is pressed & player is grounded then...
            if (Input.GetKeyDown(KeyCode.W) && grounded)
            {
                // add a velocity force going up
                /* rigidbody velocity is equal to the 
                new set vectors in pre-set x direction, and add jumpForce Variable
                */
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);


                grounded = false;
                doubleJump = true;
                //Set animation
                #region Animation Condition
                anim.SetBool("isJumping", true);
                #endregion


            }
            else if(Input.GetKeyDown(KeyCode.W) && !grounded && doubleJump)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                doubleJump = false;
            }
        }
        
        //Collision Between Player and ground
        //Set conditions for jump dependent on collision enter
        private void OnCollisionEnter2D(Collision2D col)
        {
            //if the collision object is tagged "Ground", then....
            if (col.gameObject.CompareTag("Ground"))
            {
                //set grounded bool true 
                grounded = true;
                //set doubleJump bool false
                doubleJump = false;
                //set jump animtion to false upon contact
                anim.SetBool("isJumping", false);
            }
        }
    }
}
