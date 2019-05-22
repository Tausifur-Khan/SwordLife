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
        //player lives
         public int lives = 3;

        public bool playerDeath;
        //take damage
        public float dmg;
        [Space(3)]

        [Header("Components")]
        //slider array for hp
        public Slider hpSlider;

        public Text liveTxtUI;

        public GameObject sliderFill;
       
       
        

      
        #endregion

       // private Animator deathAnim;

        private void Start()
        {
           // deathAnim = GetComponent<Animator>();

            //set current player hp to the maximum
            curHp = Mathf.Abs(maxHp);
            

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

                PlayerController player = GetComponent<PlayerController>();
                player.keyActive = false;

                sliderFill.SetActive(false);
            }
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.CompareTag("Damage") && GetComponentsInChildren<BoxCollider2D>(false).Length == 1)
            {
                curHp -= dmg;
            }
            if (col.CompareTag("Killzone"))
            {
                curHp = 0;
            }
        }
        
    }
}