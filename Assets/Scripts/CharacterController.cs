using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

namespace knight
{

    public class CharacterController : MonoBehaviour
    {
        #region Movement Variables
        public float mvSpeed = 2;
        public float jumpForce = 50;
        public float jumpDir;

        #endregion
        //2d rigidbody component
        private Rigidbody2D rb2d;
        //animation
        private Animator anim;
        //sprite renderer
        private SpriteRenderer sprite;



        // Start is called before the first frame update
        void Start()
        {
            //Refer to rigid component
            rb2d = GetComponent<Rigidbody2D>();
            //refer to animation component
            anim = GetComponent<Animator>();
            //refer to sprite renderer component
            sprite = GetComponent<SpriteRenderer>();
        }


        private void Update()
        {
            Move();
        }
        private void FixedUpdate()
        {

        }

        void Move()
        {
            // access to input axis within horizontal section
            float inputx = Input.GetAxis("Horizontal");

            //rigidbody velocity is equal to new vector2 space
            //new vector space with x value , y normal0
            rb2d.velocity = new Vector2(inputx * mvSpeed, rb2d.velocity.y);
            

            //if this key is pressed then
            if (Input.GetKeyDown(KeyCode.W))
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                //add force going up within a 2d vector 
               // rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
            }


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
    }
}
