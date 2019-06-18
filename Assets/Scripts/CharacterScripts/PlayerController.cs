using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


namespace Knight
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables

        #region Movement Variables X & Y
        [Header("Movement Variable x & y")]
        //float speed
        [SerializeField]
        private float mvSpeed;
        public float maxSpeed = 3;
        //float jumpForce
        public float jumpForce = 50;
        public float dJumpForce = 50;
        //double jump bool condition
        public bool doubleJump;
        public bool restrictMove;
        [Space(2)]
        #endregion

        #region Grounded Variables
        //Jump bool condition
        public bool[] groundCheck = new bool[2];

        public float rayDist = 0.5f;
        public Transform raycasts;

        public LayerMask groundLayer;
        #endregion

        #region Dash Variables
        [Header("Dash Variables")]
        //dashforce
        public float dashForce = 100f;
        //bool condition for dash
        public bool isDash = true;
        public bool canDash = true;

        //Dash timers min & max

        [SerializeField]
        private float dashtimer;
        public float dashMaxTime;

        [SerializeField]
        public float dashDelay;
        public float dashMaxDelay;

        [Space(2)]
        #endregion

        #region Keycodes
        [Header("Keycodes")]
        public KeyCode left = KeyCode.A;
        public KeyCode right = KeyCode.D;
        public KeyCode jump = KeyCode.W;
        public KeyCode dash = KeyCode.Space;
        public bool keyActive;
        #endregion

        #region Private/Public Variable Components
        //2d rigidbody component
        private Rigidbody2D rb2d;
        //animation
        private Animator anim;
        //sprite renderer
        public SpriteRenderer sprite;
        //particle system
        public ParticleSystem jump_ps;
        public ParticleSystem dash_ps;

        //Access Health Script
        private Health playerLife;

        //Access to Attack Script
        private Attack attack;



        #endregion

        #region KnockBack Variables
        //player knockback
        [Header("KnockBack")]
        //knock back force
        public float knockbackX;
        public float knockbackY;
        //set knockback time
        public float knockMaxTime;
        public float knockTime;
        //has the player been knocked
        public bool knocked;
        #endregion

        #endregion

        // Start is called before the first frame update
        void Start()
        {
            mvSpeed = maxSpeed;

            #region Movement Conditions
            //double jump condition false
            doubleJump = false;
            //groundCheck condition true
            groundCheck[0] = true;
            groundCheck[1] = true;


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
            playerLife = GetComponent<Health>();
            //refer to Attack() script component
            attack = GetComponent<Attack>();
            #endregion

            //dash time
            dashtimer = dashMaxTime;

            dashDelay = dashMaxDelay;

            knockTime = knockMaxTime;

            //movement condition true
            keyActive = true;

            
        }

        #region Update Functions
        //Update every frame
        void Update()
        {
            Debug.Log("Falling" + rb2d.velocity.y);
            //Jump Method
            Jump();
            //Ground Method
            Grounded();

            //Dash Move Method
            Dash();

        }

        //Update Physics based movement
        void FixedUpdate()
        {
            //Movement Method
            Move();


            //physical movement of knock
            PlayerKnockBackTime();
        }

        //Update anything end of frame
        void LateUpdate()
        {
            AnimationSetup();
            //Movement in death
            RestrictMove();
            //Dash time Method
            DashTimer();

        }
        #endregion

        //Player Animation Method
        void AnimationSetup()
        {
            #region Ground Movement
            //if movement is left or right then...
            if (rb2d.velocity.x >= 0.1 || rb2d.velocity.x <= -0.1)
                //move animation true
                anim.SetBool("isMoving", true);

            else if (rb2d.velocity.x == 0)
                //move animation false
                anim.SetBool("isMoving", false);
            #endregion

            #region Jump
            // if jumping and ground check is false then.....
            if (rb2d.velocity.y > 0 /*|| rb2d.velocity.y < 0*/ &&
                ((groundCheck[0] == false && groundCheck[1] == false)))
            {
                //jump animation true
                anim.SetBool("isJumping", true);
                //move animation false while in air
                anim.SetBool("isMoving", false);
            }
            else if (rb2d.velocity.y < 0 &&
                ((groundCheck[0] == false && groundCheck[1] == false)))
            {
                anim.SetInteger("Falling", 1);
            }

            else
            {
                //otherwise set jump animation false
                anim.SetBool("isJumping", false);
               
            }


            #endregion

            #region Death
            if (playerLife.playerDeath == true)
                anim.Play("Death");


            #region KnockBack
            if (knocked)
                anim.Play("KnockBack");
            #endregion
            #endregion

        }

        #region Character Movements
        //Method: Movement in X direction with velocity
        void Move()
        {
            //Get Key input
            #region Movement
            if (!knocked && keyActive)
            {
                //if input left
                if (Input.GetKey(left))
                {
                    //add movement left
                    rb2d.velocity = new Vector2(-mvSpeed, rb2d.velocity.y);
                    //flip sprite
                    sprite.flipX = true;
                }
                //else if input right
                else if (Input.GetKey(right))
                {
                    //add movement right
                    rb2d.velocity = new Vector2(mvSpeed, rb2d.velocity.y);
                    //flip spirte
                    sprite.flipX = false;
                }
                //otherwise
                else //stop velocity movement x
                    rb2d.velocity = new Vector2(0, rb2d.velocity.y);

                #endregion
            }
        }

        //Method: Dash movement in x direction
        void Dash()
        {
            #region Dash Movement
            //if dash bool condition true & timer is not 0 then...
            if (isDash && canDash && (groundCheck[0] && groundCheck[1]) && keyActive)
            {
                //if input key 'space' then...
                if (Input.GetKeyDown(dash) && Input.GetKey(left) ||
                    Input.GetKeyDown(dash) && Input.GetKey(right))
                {
                    dash_ps.Play();
                    //allow force for dash in right dir
                    //Change player speed temp
                    mvSpeed = dashForce;

                    isDash = false;
                    //play the particle effect
                    
                }
            }

            #endregion
        }

        //Method: Movement in Y direction with velocity & Animation
        //Jump Movement
        void Jump()
        {
            if (keyActive)
            {
                //if this key is pressed & player is grounded then...K
                if (Input.GetKeyDown(jump) && (groundCheck[0] || groundCheck[1]))
                {
                    // add a velocity force going up
                    /* rigidbody velocity is equal to the 
                    new set vectors in pre-set x direction, and add jumpForce Variable
                    */
                    rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);

                    doubleJump = true;

                }

                //else if not grounded and bool condition true & input pressed then...
                else if (Input.GetKeyDown(jump) && (groundCheck[0] == false && groundCheck[1] == false) && doubleJump)
                {
                    //Apply velocity up
                    rb2d.velocity = new Vector2(rb2d.velocity.x, dJumpForce);
                    //rb2d.velocity = Mathf.Clamp(rb2d.velocity.x, -1, 999) * Vector3.up;
                    //rb2d.AddForce(new Vector2(rb2d.velocity.x, dJumpForce), ForceMode2D.Impulse);
                    //set double jump bool condition false
                    doubleJump = false;
                    //play the particle effect
                    jump_ps.Play();
                }
            }
        }
        #endregion

        void Grounded()
        {
            // i index outside foreach
            //NOTE TO SELF, DONT DO IT AGAIN..............
            int i = 0;
            //for each raycast within parent transform
            foreach (Transform raycast in raycasts)
            {
                #region For Each in Raycasting
                // set new ray pos and direction
                Ray ray = new Ray(raycast.position, Vector2.down);

                //set raycast hit 2d objects
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, rayDist, groundLayer);

                //check if it hits something
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Ground") || hit.collider.CompareTag("Platform"))
                    {
                        //set groundCheck bool true 
                        groundCheck[i] = true;

                    }
                }
                //otherwise if rays hit nothing then..
                else
                {
                    //set bool condition false
                    groundCheck[i] = false;

                }
                //increase index by 1 per loop to check both raycast
                i++;
                #endregion
            }

        }


        void PlayerKnockBackTime()
        {
            // SpriteRenderer knockBackSprite;
            //if player is knocked then...

            if (knocked)
            {
                knockTime -= Time.deltaTime;
            }

            //if knock back time has reached 0 then...
            if (knockTime <= 0)
            {
                //player isnt in knockback faze
                knocked = false;
                //reset timer
                knockTime = knockMaxTime;
            }

        }

        #region Player KnockBack
        private void OnTriggerEnter2D(Collider2D col)
        {
            //if tag is enemy then...
            if ((col.tag == "Damage" && GetComponentsInChildren<BoxCollider2D>(false).Length == 1) && col.gameObject.layer != 12)
            {
                Camera.main.GetComponent<ScreenShake>().Shake();

                knocked = true;

                if (col.transform.position.x < transform.position.x)
                    //add knockback effect
                    rb2d.velocity = new Vector2(knockbackX, knockbackY);

                else
                    //add knockback effect
                    rb2d.velocity = new Vector2(-knockbackX, knockbackY);

            }


        }
        #endregion


        private void OnDrawGizmos()
        {
            //for each raycast within raycast parent...
            foreach (Transform raycast in raycasts)
            {
                // set new ray pos and direction
                Ray ray = new Ray(raycast.position, Vector2.down);
                //Set colour of ray red
                Gizmos.color = Color.red;
                //Draw ray
                Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * rayDist);
            }
        }

        // cooldown method
        void DashTimer()
        {
            #region Dash Timer
            //if bool condition for isDash false then...
            if (!isDash)
            {
                anim.SetBool("isDashing", true);
                //start timer
                dashtimer -= Time.deltaTime;

                if (dashtimer <= 0f)
                {
                    //isdash bool condition is true
                    isDash = true;

                    anim.SetBool("isDashing", false);

                    mvSpeed = maxSpeed;

                    //dash time is back to original start time
                    dashtimer = dashMaxTime;

                    canDash = false;
                }
            }

            if (!canDash)
            {
                dashDelay -= Time.deltaTime;
                if (dashDelay <= 0)
                {
                    canDash = true;
                    dashDelay = dashMaxDelay;

                }
            }



            #endregion
        }


        void RestrictMove()
        {
            //if bool condition true of death then...
            if (playerLife.playerDeath == true)
            {
                //Restrict any movement x & y without effecting y
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
                keyActive = false;
            }
            
            if (restrictMove == true)
            {
                //Restrict any movement x & y without effecting y
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
                //Disabler key activate
                keyActive = false;
            }
           
        }

    }

}
