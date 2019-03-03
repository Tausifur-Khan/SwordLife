using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

namespace Knight
{
    public class CharacterController : MonoBehaviour
    {
        #region Variables

        #region Movement Variables X & Y
        [Header("Movement Variable x & y")]
        //float speed 
        public float mvSpeed = 2;
        //float jumpForce
        public float jumpForce = 50;      
        //double jump bool condition
        public bool doubleJump;
        [Space(2)]
        #endregion

        #region Grounded Variables
        //Jump bool condition
        public bool grounded;
        public float rayDist = 0.5f;
        public Transform raycastPos;
        public LayerMask groundLayer;
        #endregion

        #region Dash Variables
        [Header("Dash Variables")]
        //dashforce
        public float dashForce = 100f;
        //bool condition for dash
        public bool isDash = true;
        //Dash timers min & max
        public float dashMaxTime;
        public float dashtimer;
        [Space(2)]
        #endregion

        #region Keycodes
        [Header("Keycodes")]
        public KeyCode left = KeyCode.A;
        public KeyCode right = KeyCode.D;
        #endregion

        #region Private/Public Variable Components
        //2d rigidbody component
        private Rigidbody2D rb2d;
        //animation
        private Animator anim;
        //sprite renderer
        public SpriteRenderer sprite;
        //Access Death Script
        private Death_1 playerDeath;
        //Access to Attack Script
        private Attack attack;
        #endregion


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

            #region Component references
            //Refer to rigid component
            rb2d = GetComponent<Rigidbody2D>();
            //refer to animation component
            anim = GetComponent<Animator>();
            //refer to sprite renderer component
            sprite = GetComponent<SpriteRenderer>();
            //refer to Death() script componenet
            playerDeath = GetComponent<Death_1>();
            //refer to Attack() script component
            attack = GetComponent<Attack>();
            #endregion

            //dash time
            dashtimer = dashMaxTime;
          
        }

        void Update()
        {
            //Jump Method
            Jump();
            //Ground Method
            Grounded();
        }

        void FixedUpdate()
        {
            //Movement Method
            Move();
            //Dash Move Method
            Dash();
           
        }

        void LateUpdate()
        {
            //if bool condition true of death then...
            if (playerDeath.death)
            {
                //set velocity y to 0
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);

                //set death bool conditon to false
                playerDeath.death = false;

                //Reset Sprite change
                sprite.flipX = false;
            }
            //Dash time Method
            Timer();
        }

        //Method: Movement in X direction with velocity
        void Move()
        {
            //Get Key input
            #region Alternate movement
            //if input left
            if (Input.GetKey(left))
            {
                //add movement left
                rb2d.velocity = new Vector2(-mvSpeed, rb2d.velocity.y);
                //flip sprite
                sprite.flipX = true;
                //set animation true
                anim.SetBool("isMoving", true);
                attack.enabled = false;
            }
            //else if input right
            else if (Input.GetKey(right))
            {
                //add movement right
                rb2d.velocity = new Vector2(mvSpeed, rb2d.velocity.y);
                //flip spirte
                sprite.flipX = false;
                ///set animation
                anim.SetBool("isMoving", true);
                attack.enabled = false;
            }
            //otherwise
            else
            {
                //stop velocity movement x
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
                //set animation false
                anim.SetBool("isMoving", false);
                attack.enabled = true;
            }
            #endregion

        }

        //Method: Dash movement in x direction
        void Dash()
        {
            #region Dash Movement
            //if dash bool condition true & timer is not 0 then...
            if (isDash)
            {
                //if input key 'space' then...
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // if horizontal direction is right then...
                    if (Input.GetKey(left))
                    {
                        // allow force for dash in right dir
                        rb2d.AddForce(Vector2.left * dashForce, ForceMode2D.Force);

                        isDash = false;

                    }
                    //if horizontal direction is left then...
                    if (Input.GetKey(right))
                    {
                        // allow force for dash in left dir
                        rb2d.AddForce(Vector2.right * dashForce, ForceMode2D.Force);
                        isDash = false;

                    }
                }
            }

            #endregion
        }

        //Method: Movement in Y direction with velocity & Animation
        //Jump Movement
        void Jump()
        {
            if (Input.GetKeyDown(KeyCode.W) && !grounded && doubleJump)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                doubleJump = false;
            }
            else if(Input.GetKeyDown(KeyCode.W) && grounded)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                doubleJump = true;
            }
            ////if this key is pressed & player is grounded then...K
            //if (Input.GetKeyDown(KeyCode.W) && grounded)
            //{

                //    // add a velocity force going up
                //    /* rigidbody velocity is equal to the 
                //    new set vectors in pre-set x direction, and add jumpForce Variable
                //    */
                //    rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);

                //    doubleJump = true;

                //}
                //else if (Input.GetKeyDown(KeyCode.W) && !grounded && doubleJump)
                //{
                //    rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                //    doubleJump = false;
                //}
        }

        void Grounded()
        {
            // set new ray pos and direction
            Ray ray = new Ray(raycastPos.position, Vector2.down);

            //set raycast hit 2d objects
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, rayDist, groundLayer);
            //check if it hits something
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    //set grounded bool true 
                    grounded = true;
                    //set doubleJump bool false
                    doubleJump = false;
                    //set jump animtion to false upon contact
                    anim.SetBool("isJumping", false);
                }

            }
            //otherwise if hit nothing then..
            else
            {
                //set jump animtion to false upon contact
                anim.SetBool("isJumping", true);
                grounded = false;
               
            }
        }

        private void OnDrawGizmos()
        {
            // set new ray pos and direction
            Ray ray = new Ray(raycastPos.position, Vector2.down);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * rayDist);
        }

        // cooldown method
        void Timer()
        {
            #region Dash Timer
            //if bool condition for isDash false then...
            if (!isDash)
            {
                anim.SetBool("isDashing", true);
                //start timer
                dashtimer -= Time.deltaTime;
                //if timer is less than eqaul to 0 then...
                if (dashtimer <= 0)
                {
                    anim.SetBool("isDashing", false);

                    //dash time is back to original start time
                    dashtimer = dashMaxTime;
                    //isDash conditon true
                    isDash = true;
                }
            }
            #endregion

            #region Attack Time
            //if attack bool condition is false then...
            if (!attack.canAttack)
            {
                attack.timer -= Time.deltaTime;
                if (attack.timer <= 0)
                {
                    attack.canAttack = true;
                    attack.timer = attack.attackMaxTime;
                    attack.colliders[0].enabled = false;
                    attack.colliders[1].enabled = false;
                    attack.colliders[2].enabled = false;
                    attack.colliders[3].enabled = false;
                }
            }
            #endregion 
        }
        
    }

}
