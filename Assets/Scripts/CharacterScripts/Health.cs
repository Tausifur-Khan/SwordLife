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
        public int lives;

       

        private Death playerDeath;
        //take damage
        public float dmg;
        #endregion

        private void Start()
        {
            playerDeath = GetComponent<Death>();

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
            hpSlider.value = curHp;
            liveTxtUI.text = lives.ToString();
        }


        

        void PlayerDmg()
        {
            if (curHp <= 0)
            {
                curHp = 0;
                //fill.SetActive(false);
                lives--;
                curHp = maxHp;
               // playerDeath.death = true;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                curHp -= dmg;
            }
        }
        
    }
}