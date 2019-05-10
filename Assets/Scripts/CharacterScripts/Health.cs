using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;


namespace Knight
{
    public class Health : MonoBehaviour
    {
        #region Hp Variables
        [Header("Hp Stuff")]
        //current player hp
        public float curHp;
        //maximum player hp
        public float maxHp;



        //slider array for hp
        public Slider hpSlider;

        public Text liveTxtUI;
       
        public int lives = 3;
        

        public bool playerDeath;
        //take damage
        public float dmg;
        #endregion

       // private Animator deathAnim;

        private void Start()
        {
           // deathAnim = GetComponent<Animator>();

            //set current player hp to the maximum
            curHp = Mathf.Abs(maxHp);
            

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                curHp = 0;
            }
        }

        private void LateUpdate()
        {
          
            PlayerDmg();

            //GUI stuff
            hpSlider.value = curHp;
            liveTxtUI.text = lives.ToString();
        }

        void PlayerDmg()
        {
            if (curHp <= 0 && !playerDeath)
            {
               
                //fill.SetActive(false);
                lives--;
                //curHp = maxHp;
                playerDeath = true;

                CharacterController player = GetComponent<CharacterController>();
                player.keyActive = false;
            }
        }

        
        //private void OnCollisionEnter2D(Collision2D collision)
        //{
        //    if (collision.gameObject.CompareTag("Enemy"))
        //    {
        //        curHp -= dmg;
        //    }
        //}

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.name == "melee")
            {
                curHp -= dmg;
            }

            if (col.name == "Diamond")
            {
                curHp -= dmg;
            }
        }

        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    if (collision.gameObject.name == "KillZone")
        //    {
        //        curHp = 0;
        //    }
        //}

    }
}