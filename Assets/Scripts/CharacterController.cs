using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

namespace Knight
{
    public class CharacterController : MonoBehaviour
    {
        #region Movement Variables X & Y
        [Header("Movement Variable x & y")]
        //float speed 
        public float mvSpeed = 2;
        //float jumpForce
        public float jumpForce = 50;
        //Jump condition
        public bool grounded;
        //double jump bool condition
        public bool doubleJump;
        [Space(2)]

        #region Dash Variables
        [Header("Dash Variables")]
        //dashforce
        public float dashForce = 100f;
        //bool condition for dash
        public bool isDash = true;
        //Dash timers min & max
        public float dashTime = 1;
        private float timer = 0;
        #endregion

        #endregion

        #region Private Variable Components
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
            #region Movement Conditions
            //double jump condition false
            doubleJump = false;
            //grounded condition true
            grounded = true;
            //dash condition true
            isDash = true;
            #endregion

            //Refer to rigid component
            rb2d = GetComponent<Rigidbody2D>();
            //refer to animation component
            anim = GetComponent<Animator>();
            //refer to sprite renderer component
            sprite = GetComponent<SpriteRenderer>();
        }


        void Update()
        {
            Jump();

        }

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
            rb2d.velocity = new Vector2(inputx * mvSpeed, rb2d.velocity.y);

            //if dash bool condition true then...
            if (isDash)
            {
                //Dash Movement
                //if input key 'space' then...
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isDash = false;
                    
                    // if horizontal direction is right then...
                    if (Input.GetAxis("Horizontal") >= 0.1)
                    {
                        // allow force for dash in right dir
                        rb2d.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);
                    }
                    //if horizontal direction is left then...
                    if (Input.GetAxis("Horizontal") <= -0.1)
                    {
                        // allow force for dash in left dir
                        rb2d.AddForce(Vector2.left * dashForce, ForceMode2D.Impulse);
                    }
                }
            }

            
            #region Sprite Flip Condition
            // sprite flip left
            if (inputx <= -0.1)
            {
                sprite.flipX = true;
            }

            //sprite flip right
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
            else if (Input.GetKeyDown(KeyCode.W) && !grounded && doubleJump)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                doubleJump = false;
            }

        }

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
